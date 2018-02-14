using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueCommClass {

	private Queue<ResourceMessage> choices;

	public void InjectChoices(Queue<ResourceMessage> c) {
		choices = new Queue<ResourceMessage>();
		foreach(ResourceMessage ch in c) {
			choices.Enqueue(ch);
		}
		Debug.Log("Injected queue with " + choices.Count + " members.");
	}

	public int GetCount() {
		return choices.Count;
	}
}
