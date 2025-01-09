using System.Collections.Generic;
using UnityEngine;

public class SymbolBlock : Block
{
    private enum Symbol { add, subtract, multiply, divide }

    [SerializeField] private Symbol symbol;

    private Dictionary<Symbol, string> symbolStrings = new()
    {
        {Symbol.add, "+"},
        {Symbol.subtract, "-"},
        {Symbol.multiply, "x"},
        {Symbol.divide, "/"},
    };

    private void Start()
    {
        textMeshProUGUI.text = symbolStrings[symbol];
    }

    public override bool CanCombine(Block previousBlock, Vector3 direction)
    {
        if (GameManager.Instance.BlockAtPosition(transform.position + direction, out Block block))
        {
            if (block.CanCombine(this, direction))
            {
                return true;
            }

            if (previousBlock == null)
            {
                return false;
            }

            return previousBlock.GetType() == typeof(NumberBlock) && block.GetType() == typeof(NumberBlock);
        }

        return false;
    }


}
