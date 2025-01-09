using TMPro;
using UnityEngine;
using static SymbolBlock;

public class NumberBlock : Block
{
    [SerializeField] private int value;

    public int Value => value;

    private void Start()
    {
        textMeshProUGUI.text = value.ToString();
    }

    public void SetValue(int value)
    {
        this.value = value;
        textMeshProUGUI.text = value.ToString();
    }

    public override bool CanCombine(Block previousBlock, Vector3 direction, out CombinationInfo info)
    {
        if (GameManager.Instance.BlockAtPosition(transform.position + direction, out Block block))
        {
            if (block.CanCombine(this, direction, out CombinationInfo i))
            {
                info = i;
                return true;
            }
        }

        info = new CombinationInfo();
        return false;
    }
}
