using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

//This is a storage class to hold 2 dialogs. It keeps a state depending on the dis-/approve bottun hit. It get's handled in the resourcesManager.
[Serializable]
public class Choice  {
    [Header("Dilemma: ")]
    [TextArea()]
    public string Dilemma;

	[Header("Advisor text")]
	public string AdvisorText;
    [Header("Character name: ")]
    [TextArea()]
    public string Name;

	[Header("Character: ")]
	public Characters character;
    [Header("State:")]
    public State State;
    [Header("Positive Dialog")]
    public Dialog PositiveDialog;

    [Header("Negative Dialog")]
    public Dialog NegativeDialog;

	public string GetAdvisorText() {
		return AdvisorText;
	}

}

public enum State {
    Positive,
    Negative,
    Neutral
}


