using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingResourcesManager : MonoBehaviour {

	private List<LivingResource> livingResources;

	private void Awake() {
		EventManager.AddLivingResource += AddLivingResource;
		EventManager.ExecuteLivingResources += CheckLivingResources;
	}

	public void CheckLivingResources() {
		if(livingResources.Count > 0) {
			for(int i = 0; i < livingResources.Count; i++) {
				livingResources[i].Act();
				if(livingResources[i].isToBeDestroyed)
					livingResources[i] = null;
			}
		}
		livingResources = CleanupList(livingResources);
	}

	public void AddLivingResource(params LivingResource[] res) {
		if(res != null) {
			foreach(LivingResource i in res) {
				livingResources.Add(i);
			}
		}
	}

	private List<LivingResource> CleanupList(List<LivingResource> list) {
		List<LivingResource> temp = new List<LivingResource>();
		foreach(LivingResource l in list) {
			if(l != null)
				temp.Add(l);
		}
		return temp;
	}

}

public class LivingResource {
	public int lifetime;
	public int cooldown;
	public int initCooldown;
	private ResourceMessage rm;
	public bool isToBeDestroyed = false;

	public LivingResource(Resources res, int amt, int life, int cd) {
		lifetime = life;
		cooldown = cd;
		initCooldown = cooldown;

		rm = new ResourceMessage();
		rm.Initialise(res, amt, true);
	}

	public void Act() {
		if(lifetime <= 1) {
			isToBeDestroyed = true;
		}
		else {
			if(cooldown <= 1) {
				EventManager._SendResourceMessage(rm);
				lifetime--;
				cooldown = initCooldown;
			}
			else {
				cooldown--;
			}
		}
	}
}