using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceMessageReflection : ScriptableObject{
	public List<ResourceMessage> resourceMessages;

	public void Initialise() {
		resourceMessages = new List<ResourceMessage>();
	}
}
