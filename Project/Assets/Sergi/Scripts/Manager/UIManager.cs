using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour {
    public GameObject Choice;
    public GameObject Screen;
    public GameObject Screen_Sprite;
    public GameObject Results;

    public GameObject DesicionButton;
    public GameObject ContinueButton;
    public Text Textbubble;
    public Image Character;

	private Transform currentChar;

    public Text Population;
    public Text Happiness;
    public Text Environment;
    public Text Currency;

    public Text ResultPop;
    public Text ResultHap;
    public Text ResultEnv;
    public Text ResultCur;

    //Format of: population, currency, happiness, environment
    private Vector4 resourceDeltas;
    private void Start() {
        EventManager.ChoiceLoad += LoadChoice;
        EventManager.DisplayChoice += SetChoice;
        EventManager.UIEnable += EnableUI;
        EventManager.UIDisable += DisableUI;
        EventManager.UIContinue += ContinueUI;
        EventManager.SendV4 += GetDeltas;
        Screen_Sprite.transform.DOScale(0, 1);
    }
    private void Update() {
        Population.text = "" + EventManager.Get_Population();
        Happiness.text = "" + EventManager.Get_Happiness();
        Currency.text = "" + EventManager.Get_Currency();
        Environment.text = "" + EventManager.Get_Environment();
    }

    void EnableUI() {
        Screen.SetActive(false);
        Results.SetActive(true);
        ResultPop.DOText("Population: " + resourceDeltas.w + " people", 1, true,ScrambleMode.None);
        ResultHap.DOText("Hapiness: " + resourceDeltas.y + "%", 1, true, ScrambleMode.None);
        ResultCur.DOText("Currency: " + resourceDeltas.x + " paluta", 1, true, ScrambleMode.None);
        ResultEnv.DOText("Environment: " + resourceDeltas.z + "%", 1, true, ScrambleMode.None);
        Choice.SetActive(false);

    }
    void DisableUI() {
        Screen.SetActive(true);
        Results.SetActive(false);
        Choice.SetActive(true);
    }

    //Format of: population, currency, happiness, environment
    private void GetDeltas(Vector4 v4) {
        resourceDeltas = v4;
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
        Screen_Sprite.transform.DOScale(0, 0.001f);
        Debug.Log(currentChar.localScale);
		currentChar.localPosition = Vector3.zero;
		Debug.Log(_choice.Dilemma);
        //Textbubble.text = _choice.Dilemma;
        Textbubble.DOText(_choice.Dilemma, 4, true, ScrambleMode.All);
        Screen_Sprite.transform.DOScale(1, 1f);
    }

    void SetChoice(Choice _choice) {
        DesicionButton.SetActive(false);
        ContinueButton.SetActive(true);
        switch (_choice.State) {
            case (State.Negative):
                Textbubble.DOText(_choice.NegativeDialog.text, 4, true, ScrambleMode.All);
                break;
            case (State.Positive):
                Textbubble.DOText(_choice.PositiveDialog.text, 4, true, ScrambleMode.All);
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
