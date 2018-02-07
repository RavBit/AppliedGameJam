using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {
    public delegate void ChoiceState (Choice _choice);
    public static event ChoiceState ChoiceLoad;
    public static event ChoiceState DisplayChoice;
    public static event ChoiceState ChoiceUnLoad;

	public delegate void ResourceEvent(params ResourceMessage[] res);
	public static event ResourceEvent SendResourceMessage;
	public static event ResourceEvent EnqueueMessageEvent;

	public delegate void ChooseEvent();
    public static event ChooseEvent ChoosePositive;
    public static event ChooseEvent ChooseNegative;
    public static void Choice_Load(Choice _choice) {
        ChoiceLoad(_choice);
    }
    public static void Choice_Unload(Choice _choice) {
        ChoiceLoad(_choice);
    }
    public static void Choose_Choice(int state) {
        switch(state) {
            case (0):
                ChooseNegative();
                break;
            case (1):
                ChoosePositive();
                break;

        }
    }
    public static void Display_Choice(Choice _choice) {
        DisplayChoice(_choice);
    }
	public static void _SendResourceMessage(params ResourceMessage[] res) {
		SendResourceMessage(res);
	}
	public static void _EnqueueMessage(params ResourceMessage[] res) {
		SendResourceMessage(res);
	}
}
