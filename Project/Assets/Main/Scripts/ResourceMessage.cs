using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ResourceMessage")]
public class ResourceMessage : ScriptableObject {
	public Resources resourceType;
	public bool isToday = true;
	public int amount;
	public ResourceMessage(Resources t, int i, bool b) {
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
