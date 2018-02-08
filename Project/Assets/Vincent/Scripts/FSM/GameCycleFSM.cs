using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCycleFSM {

	private BaseState currentState;
	public Queue<ResourceMessage> queueToHandle;

	public GameCycleFSM(BaseState startState) {
		if(startState != null) {
			SetState(startState);
		}
	}

	public void SetState(BaseState newState) {
		if(currentState != null) {
			currentState.onState -= SetState;
			currentState.Exit(queueToHandle);
		}

		newState.Entry(queueToHandle);
		newState.onState += SetState;

		currentState = newState;
	}

	public void Run() {
		if(currentState != null) {
			currentState.Stay(queueToHandle);
		}
	}

}
