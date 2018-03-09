using UnityEngine;
using System;

//This class is a storage container which is meant to pass on resource changes.
[Serializable]
public class Dialog{
    [Header("Influence the following items")]
	public ResourceMessage[] messages;

    [Header("Dialog:")]
    [TextArea()]
    public string text;

}