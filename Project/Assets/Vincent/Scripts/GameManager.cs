using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	private Queue<ResourceMessage> currentDay = new Queue<ResourceMessage>();
	private Queue<ResourceMessage> nextDay = new Queue<ResourceMessage>();
	private GameCycleFSM fsm;

	public void Awake() {
		EventManager.EnqueueMessageEvent += EnqueueMessage;
		fsm = new GameCycleFSM(new DayState());
	}

	private void Update() {
		fsm.Run();
	}

	private void NewDay() {
		ExecuteDay();
		currentDay = nextDay;
		nextDay = new Queue<ResourceMessage>();
	}

	private void ExecuteDay() {
		ResourceMessage[] tempArray = new ResourceMessage[currentDay.Count];
		for(int i = 0; i < tempArray.Length; i++) {
			tempArray[i] = currentDay.Dequeue();
		}
		EventManager._SendResourceMessage(tempArray);
	}

	public void EnqueueMessage(params ResourceMessage[] rs) {
		foreach(ResourceMessage rm in rs) {
			if(rm.GetIsToday())
				currentDay.Enqueue(rm);
			else
				nextDay.Enqueue(rm);
		}
	}
}
