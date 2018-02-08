using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {
    public delegate void ChoiceState (Choice _choice);
    public static event ChoiceState ChoiceLoad;
    public static event ChoiceState DisplayChoice;
    public static event ChooseEvent ChoiceUnLoad;

	public delegate void ResourceEvent(params ResourceMessage[] res);
	public static event ResourceEvent SendResourceMessage;
	public static event ResourceEvent EnqueueMessageEvent;

	public delegate void AdvanceEvent();
	public static event AdvanceEvent NextDay;

	public delegate Queue<ResourceMessage> QueueEvent();
	public static event QueueEvent GetQueue;

	public delegate void ChooseEvent();
    public static event ChooseEvent ChoosePositive;
    public static event ChooseEvent ChooseNegative;
    public static event ChooseEvent UIEnable;
    public static event ChooseEvent UIDisable;
    public static event ChooseEvent UIContinue;
	public static event ChooseEvent EmptyQueue;
    public static void Choice_Load(Choice _choice) {
        ChoiceLoad(_choice);
    }

    public static void Choice_Unload() {
        ChoiceUnLoad();
    }
	
	public static Queue<ResourceMessage> Get_Queue() {
		return GetQueue();
	}
    public static void InterMission_Enable() {
        UIEnable();
    }
    public static void InterMission_Disable() {
		UIDisable();
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
		EnqueueMessageEvent(res);
	}
	public static void _ChoiceLoad() {
		ChoiceUnLoad();
	}
	public static void _NextDay() {
		NextDay();
	}
}
