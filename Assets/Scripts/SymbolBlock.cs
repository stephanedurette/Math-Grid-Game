using System.Collections.Generic;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class SymbolBlock : Block
{
    public enum Symbol { add, subtract, multiply, divide }

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

    public override bool CanCombine(Block previousBlock, Vector3 direction, out CombinationInfo info)
    {
        if (GameManager.Instance.BlockAtPosition(transform.position + direction, out Block nextBlock))
        {
            if(ValidCombination(previousBlock, nextBlock))
            {
                info = new CombinationInfo();
                info.Operator_A = (previousBlock as NumberBlock).Value;
                info.Operator_B = (nextBlock as NumberBlock).Value;
                info.Operand = symbol;
                info.ResultPosition = transform.position + direction;

                return true;
            }

            if (nextBlock.CanCombine(this, direction, out CombinationInfo i))
            {
                info = i;
                return true;
            }
        }

        info = new CombinationInfo();
        return false;
    }

    bool ValidCombination(Block previous, Block next) => previous != null && previous.GetType() == typeof(NumberBlock) && next.GetType() == typeof(NumberBlock);

    public struct CombinationInfo
    {
        public int Operator_A;
        public int Operator_B;
        public Symbol Operand;
        public Vector3 ResultPosition;
    }
}
