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


}
