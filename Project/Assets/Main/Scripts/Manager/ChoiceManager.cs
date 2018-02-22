﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script manages the placing and removal of choices in the gamescene.
public class ChoiceManager : MonoBehaviour {
    public List<Choice> Choices;
    public Choice curchoice;
    public int choicecounter = -1;
    public Queue<ResourceMessage> ChoiceQueue = new Queue<ResourceMessage>();

    private void Start() {
        EventManager.ChoiceLoad += LoadChoice;
        EventManager.ChoosePositive += PositiveChoice;
        EventManager.ChooseNegative += NegativeChoice;
        EventManager.ChoiceUnLoad += UnLoadChoice;
        EventManager.GetQueue += Get_Queue;
        EventManager.NextDay += ResetQueue;
        EventManager.NightCycle += BeginNight;
        Invoke("Initialize", .000001f);
    }

	private Queue<ResourceMessage> Get_Queue() {
        return ChoiceQueue;
    }

    private void ResetQueue() {
        ChoiceQueue = new Queue<ResourceMessage>();
    }

	private void BeginNight() {
        Invoke("EndNight", 3);
    }

	private void EndNight() {
        EventManager.InterMission_Continue();
    }
	private void Initialize() {
		EventManager.Choice_Load(Choices[choicecounter]);
	}
	private void PositiveChoice() {
        curchoice.State = State.Positive;
        EventManager.Display_Choice(curchoice);
    }
	private void NegativeChoice() {
        curchoice.State = State.Negative;
        EventManager.Display_Choice(curchoice);
    }
	private void LoadChoice(Choice _choice) {
        curchoice = _choice;
    }

	//This function removes the current choice from the gamescene and submits all the resourceMessages to the gameManager.
    private void UnLoadChoice() {
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