using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	private Queue<ResourceMessage> currentDay = new Queue<ResourceMessage>();
	private Queue<ResourceMessage> nextDay = new Queue<ResourceMessage>();

	/*
	 * current -> afhandelen
	  CURRENT = tomorrow
	  tomorrow = new
	 */

	private void NewDay() {
		currentDay = nextDay;
		nextDay = new Queue<ResourceMessage>();
		ExecuteDay();
	}

	private void ExecuteDay() {
		ResourceMessage[] tempArray = new ResourceMessage[currentDay.Count];
		for(int i = 0; i < tempArray.Length; i++) {
			tempArray[i] = currentDay.Dequeue();
		}
		EventManager._SendResourceMessage(tempArray);
	}
}
