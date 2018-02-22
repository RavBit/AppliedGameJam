using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//This class is used to store all resourcemessages for when it's supposed to be handled, and then handle them.
public class GameManager : MonoBehaviour {

	private Queue<ResourceMessage> currentDay = new Queue<ResourceMessage>();
	private Queue<ResourceMessage> nextDay = new Queue<ResourceMessage>();
	private GameCycleFSM fsm;

	public void Awake() {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        EventManager.EnqueueMessageEvent += EnqueueMessage;
		EventManager.NextDay += NewDay;
		EventManager.EndGame += GameEnd;
		fsm = new GameCycleFSM(new DayState());
	}

	//Mainly used for the state-machine to operate. 
	private void LateUpdate() {
		fsm.queueToHandle = EventManager.Get_Queue();
		fsm.Run();
	}

	//Gets called when the state-machine starts a new day.
	private void NewDay() {
		ExecuteDay();
		currentDay = nextDay;
		nextDay = new Queue<ResourceMessage>();
	}

	//Processes all resourcechanges.
	private void ExecuteDay() {
		ResourceMessage[] tempArray = new ResourceMessage[currentDay.Count];
		for(int i = 0; i < tempArray.Length; i++) {
			tempArray[i] = currentDay.Dequeue();
		}
		if(tempArray.Length > 0)
			EventManager._SendResourceMessage(tempArray);
	}

	//Sends recieved resourceMessages to the resourceManager to be handled at the end of the gameLoop.
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

	//Depensing on what factor killed the planet, the relevant end-scene gets loaded in.
	public void GameEnd(Resources r) {
		switch(r) {
			case Resources.airPollution:
				SceneManager.LoadScene("AirEnd");
				break;
			case Resources.soilPollution:
				SceneManager.LoadScene("SoilEnd");
				break;
			case Resources.waterPollution:
				SceneManager.LoadScene("WaterEnd");
				break;
			case Resources.landUse:
				SceneManager.LoadScene("LandEnd");
				break;
			case Resources.biodiversity:
				SceneManager.LoadScene("BioEnd");
				break;
			case Resources.currency:
				SceneManager.LoadScene("CurEnd");
				break;
			case Resources.population:
				SceneManager.LoadScene("PopEnd");
				break;
			default:
				break;
		}
	}
}

/*
 * SceneManager.LoadScene("PopEnd");
 * SceneManager.LoadScene("CurEnd");
 * SceneManager.LoadScene("HapEnd");
 * SceneManager.LoadScene("EnvEnd");
 */
