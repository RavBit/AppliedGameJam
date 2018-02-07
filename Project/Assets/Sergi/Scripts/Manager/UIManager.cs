using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public Text Textbubble;

    private void Start() {
        EventManager.ChoiceLoad += LoadChoice;
        EventManager.DisplayChoice += SetChoice;
    }

    void LoadChoice(Choice _choice) {
		Debug.Log("Reached");
		Debug.Log(_choice.Dilemma);
        Textbubble.text = _choice.Dilemma;
    }

    void SetChoice(Choice _choice) {
        //Debug.Log("Choice state");
        switch (_choice.State) {
            case (State.Negative):
                Textbubble.text = _choice.NegativeDialog.text;
                break;
            case (State.Positive):
                Textbubble.text = _choice.PositiveDialog.text;
                break;
        }
    }

    public void Choose(int state) {
        EventManager.Choose_Choice(state);
    }
}
