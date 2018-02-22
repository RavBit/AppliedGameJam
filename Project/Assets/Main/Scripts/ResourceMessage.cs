using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is a storage class that contains the values to change for a single resource.
[CreateAssetMenu(menuName = "ResourceMessage")]
public class ResourceMessage : ScriptableObject {
	public Resources resourceType;
	public bool isToday = true;
	public int amount;
	public void Initialise(Resources t, int i, bool b) {
		resourceType = t;
		amount = i;
		isToday = b;
	}

	public Resources GetResourceType() {
		return resourceType;
	}

	public int GetAmount() {
		return amount;
	}

	public bool GetIsToday() {
		return isToday;
	}
}
