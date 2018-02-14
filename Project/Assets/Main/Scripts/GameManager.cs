using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	private Queue<ResourceMessage> currentDay = new Queue<ResourceMessage>();
	private Queue<ResourceMessage> nextDay = new Queue<ResourceMessage>();
	private GameCycleFSM fsm;

	public void Awake() {
		EventManager.EnqueueMessageEvent += EnqueueMessage;
		EventManager.NextDay += NewDay;
		EventManager.EndGame += GameEnd;
		fsm = new GameCycleFSM(new DayState());
	}

	private void LateUpdate() {
		fsm.queueToHandle = EventManager.Get_Queue();
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
		if(tempArray.Length > 0)
			EventManager._SendResourceMessage(tempArray);
	}

	public void EnqueueMessage(params ResourceMessage[] rs) {
		if(rs.Length > 0) {
			Debug.Log(rs.Length);
			foreach(ResourceMessage rm in rs) {
				if(rm != null) {
					if(rm.GetIsToday())
						currentDay.Enqueue(rm);
					else
						nextDay.Enqueue(rm);
				}
			}
		}
		Debug.Log(currentDay.Count);
		Debug.Log(nextDay.Count);
	}

	public void GameEnd(Resources r) {
		switch(r) {
			case Resources.population:
				SceneManager.LoadScene("PopEnd");
				break;
			case Resources.currency:
				SceneManager.LoadScene("CurEnd");
				break;
			case Resources.happiness:
				SceneManager.LoadScene("HapEnd");
				break;
			case Resources.environment:
				SceneManager.LoadScene("EnvEnd");
				break;
			default:
				break;
		}
	}
}
