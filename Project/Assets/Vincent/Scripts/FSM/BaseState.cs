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
	public override void Entry(Queue<ResourceMessage> messages) {
		choicesMade = 0;
		EventManager.ChoiceUnLoad += IncreaseCount;
	}
	public override void Stay(Queue<ResourceMessage> messages) {
		if(choicesMade >= 3) {
			onState(new NightState());
		}
		else {
			//EventManager._ChoiceLoad();
		}
	}

	public override void Exit(Queue<ResourceMessage> messages) {
		EventManager.ChoiceUnLoad -= IncreaseCount;
	}

	public void IncreaseCount(Choice c) {
		choicesMade++;
	}
}

//Exists to convert the choice queue to resourceMessages. When done, it will pass these on to the next state.
public class NightState : BaseState {
	private ResourceMessage[] rm;
	public override void Entry(Queue<ResourceMessage> messages) {
		rm = new ResourceMessage[messages.Count];
		for(int i = 0; i < messages.Count; i++) {
			rm[i] = messages.Dequeue();
		}
		EventManager._SendResourceMessage(rm);
		onState(new BetweenState());
	}

	//Stay not needed here.

	public override void Exit(Queue<ResourceMessage> messages) {
		
	}
}

//Exists to give time between the choice days. the game starts in this state.
public class BetweenState : BaseState {
	private bool isDone;
	public override void Entry(Queue<ResourceMessage> messages) {
		isDone = false;
	}
	public override void Stay(Queue<ResourceMessage> messages) {
		if(isDone)
			onState(new DayState());
	}

	public override void Exit(Queue<ResourceMessage> messages) {

	}

	public void UpdateBetweenState() {
		isDone = true;
	}

}

