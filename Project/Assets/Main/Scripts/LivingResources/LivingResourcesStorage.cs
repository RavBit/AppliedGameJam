using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using Newtonsoft.Json;

public class LivingResourcesStorage : MonoBehaviour {

	[SerializeField]
	private string dataPath;
	[SerializeField]
	private LivingResourcesContainer resources;

	public static LivingResourcesStorage instance;

	private void Awake() {
		if(instance == null)
			instance = this;
		else {
			Destroy(this);
		}
		dataPath = Application.streamingAssetsPath + "/LivingResources.json";
		LoadJson();
	}

	[ContextMenu("LoadFromJson")]
	private void LoadJson() {
		using(StreamReader stream = new StreamReader(dataPath)) {
			string json = stream.ReadToEnd();
			resources = JsonConvert.DeserializeObject<LivingResourcesContainer>(json);
		}
	}

	public LivingResource GetLRWithID(int id) {
		if(id < 0 || id > resources.livingResources.Length - 1)
			return null;
		return resources.livingResources[id];
	}
}


[Serializable]
public class LivingResourcesContainer {
	public LivingResource[] livingResources;
}

[Serializable]
public class LivingResource {
	public int lifetime;
	public int cooldown;
	public int initCooldown;
	public Resources res;
	public int amount;
	public bool isToBeDestroyed = false;
	public string breakingNews;

	public LivingResource(Resources res, int amt, int life, int cd, string bn = "") {
		lifetime = life;
		cooldown = cd;
		initCooldown = cooldown;
		amount = amt;
		this.res = res;
		breakingNews = bn;
	}

	public void Act() {
		if(lifetime < 1) {
			isToBeDestroyed = true;
		}
		else {
			if(cooldown <= 1) {
				ResourceMessage rm = new ResourceMessage();
				rm.Initialise(res, amount);
				EventManager._SendResourceMessage(rm);
				EventManager._SendBreakingNews(breakingNews);
				lifetime--;
				cooldown = initCooldown;
				Debug.Log($"Executing living resource {breakingNews} with lifetime left: {lifetime}");
				cooldown--;
			}
			else {
				cooldown--;
			}
		}
	}
}
