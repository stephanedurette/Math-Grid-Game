using TMPro;
using UnityEngine;

public class NumberBlock : Block
{
    [SerializeField] private int value;

    private void Start()
    {
        textMeshProUGUI.text = value.ToString();
    }
}
