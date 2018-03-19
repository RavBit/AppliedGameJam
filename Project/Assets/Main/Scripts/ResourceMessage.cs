using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//This is a storage class that contains the values to change for a single resource.
[CreateAssetMenu(menuName = "ResourceMessage")]
[Serializable]
public class ResourceMessage {
	public Resources resourceType;
	public int amount;

	public int Amount {
		get {
			if(lrID >= 0) {
				lr = LivingResourcesStorage.instance.GetLRWithID(lrID);
			}
			return amount;
		}
	}

	public int lrID;
	public LivingResource lr;

	public void Initialise(Resources t, int i) {
		resourceType = t;
		amount = i;
	}

	public Resources GetResourceType() {
		return resourceType;
	}

	public int GetAmount() {
		return amount;
	}
}
