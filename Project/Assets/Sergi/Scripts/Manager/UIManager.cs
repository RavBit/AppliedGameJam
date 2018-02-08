using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour {
    public GameObject Choice;
    public GameObject DesicionButton;
    public GameObject ContinueButton;
    public Text Textbubble;
    public Image Character;

    private void Start() {
        EventManager.ChoiceLoad += LoadChoice;
        EventManager.DisplayChoice += SetChoice;
        EventManager.UIEnable += EnableUI;
        EventManager.UIDisable += DisableUI;
        EventManager.UIContinue += ContinueUI;
    }


    void EnableUI() {
        //NACHT

    }
    void DisableUI() {
        //DAG
    }

    void ContinueUI() {
    }
    void LoadChoice(Choice _choice) {
        Choice.SetActive(true);
        DesicionButton.SetActive(true);
        ContinueButton.SetActive(false);
        Character.sprite = _choice.Character;
		Debug.Log(_choice.Dilemma);
        Textbubble.text = _choice.Dilemma;
    }

    void SetChoice(Choice _choice) {
        DesicionButton.SetActive(false);
        ContinueButton.SetActive(true);
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
    public void Continue() {
        Choice.SetActive(false);
        EventManager.Day_Cycle();
        EventManager.Choice_Unload();
    }
}
