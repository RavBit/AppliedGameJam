using UnityEngine;

[System.Serializable]
public class Dialog {
    [Header("Influence the following items")]
    [Range(-50, 50)]
    public int currency;
    [Range(-50, 50)]
    public int health;
    [Range(-50, 50)]
    public int population;
    [Range(-50, 50)]
    public int environment;

    [Header("Dialog:")]
    [TextArea()]
    public string text;
}
