using DG.Tweening;
using TMPro;
using UnityEngine;

public class Block : MonoBehaviour
{
    protected TextMeshProUGUI textMeshProUGUI;

    private void Awake()
    {
        textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
    }

    public bool CanMove(Vector3 direction)
    {
        Vector3 newPosition = transform.position + direction;

        if (GameManager.Instance.BlockAtPosition(newPosition, out Block block))
        {
            return block.CanMove(direction);
        }

        return GameManager.Instance.GroundTileAtPosition(newPosition);
    }

    public void Move(Vector3 direction)
    {
        Vector3 newPosition = transform.position + direction;

        if (GameManager.Instance.BlockAtPosition(newPosition, out Block block))
        {
            block.Move(direction);
        }

        transform.DOMove(newPosition, GameManager.Instance.MoveDelay, false);

        //transform.position += direction;
    }
}
