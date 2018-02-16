using UnityEngine;

//This class is a storage container which is meant to pass on resource changes.
[System.Serializable]
public class Dialog : ScriptableObject{
    [Header("Influence the following items")]
	public ResourceMessage[] messages;

    [Header("Dialog:")]
    [TextArea()]
    public string text;

}
