using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Choice")]
public class Choice : ScriptableObject {
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