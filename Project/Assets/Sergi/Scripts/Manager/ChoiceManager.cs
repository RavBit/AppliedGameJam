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
        EventManager.Choice_Load(Choices[0]);
        EventManager.ChoosePositive += PositiveChoice;
        EventManager.ChooseNegative += NegativeChoice;
        EventManager.DisplayChoice += UnLoadChoice;
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
        ChoiceQueue.Enqueue(_choice);
    }
}
