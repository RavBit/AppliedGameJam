using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
[CreateAssetMenu(menuName = "Choice")]
public class Choice : ScriptableObject {
    [Header("Dilemma: ")]
    [TextArea()]
    public string Dilemma;

    [Header("Character name: ")]
    [TextArea()]
    public string Name;

    [Header("Character sprite: ")]
    public GameObject Character;
    [Header("State:")]
    public State State;
    [Header("Positive Dialog")]
    public Dialog PositiveDialog;

    [Header("Negative Dialog")]
    public Dialog NegativeDialog;

}

public enum State {
    Positive,
    Negative,
    Neutral
}