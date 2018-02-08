using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceManager : MonoBehaviour {
    public List<Choice> Choices;
    public Choice curchoice;
    public Queue ChoiceQueue = new Queue();

    private void Start() {
        EventManager.ChoiceLoad += LoadChoice;
        //Load Choice for now
        EventManager.ChoosePositive += PositiveChoice;
        EventManager.ChooseNegative += NegativeChoice;
        EventManager.DisplayChoice += UnLoadChoice;
		Invoke("Test", .000001f);
    }
	void Test() {
		EventManager.Choice_Load(Choices[0]);
	}
    void PositiveChoice() {
        curchoice.State = State.Positive;
        EventManager.Display_Choice(curchoice);
    }
    void NegativeChoice() {
        curchoice.State = State.Negative;
        EventManager.Display_Choice(curchoice);
    }
    void LoadChoice(Choice _choice) {
        curchoice = _choice;
    }

    void UnLoadChoice(Choice _choice) {
        if(_choice.State == State.Positive) {
            foreach (ResourceMessage rm in _choice.PositiveDialog.messages) {
                ChoiceQueue.Enqueue(rm);
            }
        }
        if (_choice.State == State.Negative)
            foreach (ResourceMessage rm in _choice.NegativeDialog.messages) {
                ChoiceQueue.Enqueue(rm);
            }
        Debug.Log("Choice QUEUUEU  " + ChoiceQueue.Count);
    }
}
