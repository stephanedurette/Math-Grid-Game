using DG.Tweening;
using ImprovedTimers;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Block controlledBlock;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private float moveDelay = 0.1f;

    public float MoveDelay => moveDelay;

    readonly float GRID_SIZE = 1;

    public InputSystem_Actions inputs;

    public static GameManager Instance { get; private set; }

    private CountdownTimer moveTimer;

    private bool canMove = true;

    private Vector2 moveInputVector => inputs.Player.Move.ReadValue<Vector2>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(this.gameObject);
        }

        inputs = new InputSystem_Actions();
        moveTimer = new CountdownTimer(moveDelay);
    }

    private void Start()
    {
        moveTimer.Start();
    }

    private void OnEnable()
    {
        inputs.Enable();
        moveTimer.OnTimerStart += UpdateMove;
        moveTimer.OnTimerStop += () => moveTimer.Start();
    }

    private void UpdateMove()
    {
        if (!canMove) return;

        Vector3 moveVector = GetMoveVector();

        if (moveVector == Vector3.zero) return;

        if (controlledBlock.CanCombine(null, moveVector, out SymbolBlock.CombinationInfo info))
        {
            CombineBlocks(info);
            return;
        }

        if (controlledBlock.CanMove(moveVector)){
            controlledBlock.Move(moveVector);
        }
    }

    private void CombineBlocks(SymbolBlock.CombinationInfo info)
    {
        canMove = false;

        DOTween.Sequence()
            .Append(info.OperatorBlock_A.transform.DOMove(info.OperatorBlock_B.transform.position, moveDelay))
            .Join(info.OperandBlock.transform.DOMove(info.OperatorBlock_B.transform.position, moveDelay))
            .AppendCallback(() => 
            {
                info.OperatorBlock_A.SetValue(info.Result);
                Destroy(info.OperatorBlock_B.gameObject);
                Destroy(info.OperandBlock.gameObject);
                canMove = true;
            }
            ).Play();
    }

    public bool BlockAtPosition(Vector3 position, out Block block)
    {
        Ray ray = Camera.main.ScreenPointToRay(Camera.main.WorldToScreenPoint(position));

        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            if (raycastHit.collider.gameObject.TryGetComponent(out Block b))
            {
                block = b;
                return true;
            }
        }

        block = null;
        return false;
    }

    public bool GroundTileAtPosition(Vector3 position)
    {
        TileBase tile = tilemap.GetTile(tilemap.WorldToCell(position));
        if (tile == null) return false;

        TileData tileData = new TileData();
        tile.GetTileData(Vector3Int.FloorToInt(position), tilemap, ref tileData);

        return tileData.sprite.name == "Ground";
    }

    private Vector3 GetMoveVector()
    {
        Vector3 moveVector = moveInputVector * GRID_SIZE;

        if (moveVector.x != 0)
            moveVector.y = 0;

        return moveVector;
    }
}
