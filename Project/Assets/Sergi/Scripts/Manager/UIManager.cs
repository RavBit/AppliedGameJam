using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour {
    public GameObject Choice;
    public GameObject Screen;
    public GameObject DesicionButton;
    public GameObject ContinueButton;
    public Text Textbubble;
    public Image Character;
    public Text Population;
    public Text Happiness;
    public Text Environment;
    public Text Currency;

    private void Start() {
        EventManager.ChoiceLoad += LoadChoice;
        EventManager.DisplayChoice += SetChoice;
        EventManager.UIEnable += EnableUI;
        EventManager.UIDisable += DisableUI;
        EventManager.UIContinue += ContinueUI;
    }
    private void Update() {
        Population.text = "" + EventManager.Get_Population();
        Happiness.text = "" + EventManager.Get_Happiness() + "%";
        Currency.text = "" + EventManager.Get_Currency();
        Environment.text = "" + EventManager.Get_Environment() + "%";
    }

    void EnableUI() {
        //NACHT
        Choice.SetActive(false);
        Screen.SetActive(false);

    }
    void DisableUI() {
        //DAG
        Choice.SetActive(true);
        Screen.SetActive(true);
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
