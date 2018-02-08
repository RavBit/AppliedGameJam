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

	private Transform currentChar;

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
        Happiness.text = "" + EventManager.Get_Happiness();
        Currency.text = "" + EventManager.Get_Currency();
        Environment.text = "" + EventManager.Get_Environment();
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
		if(currentChar != null)
			DestroyImmediate(currentChar.gameObject);
        Choice.SetActive(true);
        DesicionButton.SetActive(true);
        ContinueButton.SetActive(false);
		currentChar = Instantiate(_choice.Character).transform;
		Character.enabled = false;
		currentChar.parent = Character.transform;
		Debug.Log(currentChar.localScale);
		currentChar.localScale = new Vector3(0.02f, 0.036f, 0.02f);
		Debug.Log(currentChar.localScale);
		currentChar.localPosition = Vector3.zero;
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
