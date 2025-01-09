using TMPro;
using UnityEngine;

public class NumberBlock : Block
{
    [SerializeField] private int value;

    private void Start()
    {
        textMeshProUGUI.text = value.ToString();
    }

    public override bool CanCombine(Block previousBlock, Vector3 direction)
    {
        if (GameManager.Instance.BlockAtPosition(transform.position + direction, out Block block))
        {
            if (block.CanCombine(this, direction))
            {
                return true;
            }
        }

        return false;
    }
}
