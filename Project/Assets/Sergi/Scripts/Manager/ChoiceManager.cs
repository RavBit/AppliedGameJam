using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceManager : MonoBehaviour {
    public List<Choice> Choices;
    public Choice curchoice;
    public int choicecounter = 0;
    public Queue ChoiceQueue = new Queue();

    private void Start() {
        EventManager.ChoiceLoad += LoadChoice;
        //Load Choice for now
        EventManager.ChoosePositive += PositiveChoice;
        EventManager.ChooseNegative += NegativeChoice;
        EventManager.ChoiceUnLoad += UnLoadChoice;
		Invoke("Test", .000001f);
    }
	void Test() {
		EventManager.Choice_Load(Choices[choicecounter]);
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

    void UnLoadChoice() {
        choicecounter++;
        if (curchoice.State == State.Positive) {
            foreach (ResourceMessage rm in curchoice.PositiveDialog.messages) {
                ChoiceQueue.Enqueue(rm);
            }
        }
        if (curchoice.State == State.Negative) {
            foreach (ResourceMessage rm in curchoice.NegativeDialog.messages) {
                ChoiceQueue.Enqueue(rm);
            }

        }

        EventManager.Choice_Load(Choices[choicecounter]);
    }
}
