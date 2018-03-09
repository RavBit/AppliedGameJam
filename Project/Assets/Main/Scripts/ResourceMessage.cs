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
	public int lrBaseChange;
	public float lrModChange;
	public LivingResource lr;

	public void Initialise(Resources t, int i, int lrBase, float lrMod, LivingResource l = null) {
		resourceType = t;
		amount = i;
		lr = l;
		lrBaseChange = lrBase;
		lrModChange = lrMod;
	}

	public Resources GetResourceType() {
		return resourceType;
	}

	public int GetAmount() {
		return amount;
	}
}
