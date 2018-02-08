using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Dialog option")]
public class Dialog : ScriptableObject{
    [Header("Influence the following items")]
	public ResourceMessage[] messages;

    [Header("Dialog:")]
    [TextArea()]
    public string text;

}
