using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Choice  {
    [Header("Dilemma: ")]
    [TextArea()]
    public string Dilemma;

    [Header("State:")]
    public State State;
    [Header("Positive Dialog")]
    public Dialog PositiveDialog;

    [Header("Positive Dialog")]
    public Dialog NegativeDialog;

}

public enum State {
    Positive,
    Negative,
    Neutral
}