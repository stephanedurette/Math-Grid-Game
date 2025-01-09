using TMPro;
using UnityEngine;

public class NumberBlock : Block
{
    [SerializeField] private int value;
    
    private TextMeshProUGUI textMeshProUGUI;


    private void Awake()
    {
        textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        textMeshProUGUI.text = value.ToString();
    }
}
