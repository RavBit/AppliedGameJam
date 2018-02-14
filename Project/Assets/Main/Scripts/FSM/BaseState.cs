using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void StateEvent(BaseState state);

public abstract class BaseState {

	public StateEvent onState;
	private Queue<ResourceMessage> choices = new Queue<ResourceMessage>();

	public virtual void Entry(Queue<ResourceMessage> messages) { }
	public virtual void Stay(Queue<ResourceMessage> messages) { }
	public virtual void Exit(Queue<ResourceMessage> messages) { }
}


//Exists to act as the gameloop. Will proceed when all choices have been handled.
public class DayState : BaseState {
	private int choicesMade;
	private bool isChanged = true;
	public override void Entry(Queue<ResourceMessage> messages) {
		choicesMade = 0;
		EventManager.ChoiceUnLoad += IncreaseCount;
		
		Debug.Log("Entered day state");
	}
	public override void Stay(Queue<ResourceMessage> messages) {
		if(choicesMade >= 3) {
            //Event aanroepen
            EventManager.Night_Cycle();
			Debug.Log("Reached");
			onState(new NightState());
		}
		else {
			if(!isChanged) {
				EventManager._ChoiceLoad();
				isChanged = false;
			}
		}
	}

	public override void Exit(Queue<ResourceMessage> messages) {
		EventManager.ChoiceUnLoad -= IncreaseCount;
		Debug.Log("Changing state to Night");
	}

	public void IncreaseCount() {
		choicesMade++;
		isChanged = true;
		Debug.Log("Choice made!");
		Debug.Log(choicesMade);
	}
}

//Exists to convert the choice queue to resourceMessages. When done, it will pass these on to the next state.
public class NightState : BaseState {
	private ResourceMessage[] rm;
	private bool isDone = false;
	public override void Entry(Queue<ResourceMessage> messages) {
		Debug.Log("Entered night state");
		if(messages != null) {
			Debug.Log("Been here");
			rm = new ResourceMessage[messages.Count];
			int temp = messages.Count;
			for(int i = 0; i < temp; i++) {
				rm[i] = messages.Dequeue();
			}
			EventManager._EnqueueMessage(rm);
		}
		isDone = true;
	}

	public override void Stay(Queue<ResourceMessage> messages) {
		if(isDone)
			onState(new BetweenState());
	}
	public override void Exit(Queue<ResourceMessage> messages) {
		EventManager._NextDay();
		Debug.Log("Changing state to Between");
	}
}

//Exists to give time between the choice days. the game starts in this state.
public class BetweenState : BaseState {
	private bool isDone;
	public override void Entry(Queue<ResourceMessage> messages) {
		Debug.Log("Entered between state");
		isDone = false;
        EventManager.InterMission_Enable();
        EventManager.UIContinue += UpdateBetweenState;
		
	}
	public override void Stay(Queue<ResourceMessage> messages) {
		if(isDone)
			onState(new DayState());
	}

	public override void Exit(Queue<ResourceMessage> messages) {
		//Send notice upwards to disable intermission UI
		EventManager.UIContinue -= UpdateBetweenState;
		Debug.Log("Changing state to Day");
	}

	public void UpdateBetweenState() {
		isDone = true;
	}

}

