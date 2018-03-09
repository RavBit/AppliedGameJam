using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingResourcesManager : MonoBehaviour {

	private List<LivingResource> livingResources;

	private void Awake() {
		//If you want to add a living resource
		EventManager.AddLivingResource += AddLivingResource;

		//Called at the end of the day
		EventManager.ExecuteLivingResources += DailyResources;
		EventManager.ExecuteLivingResources += CheckLivingResources;
	
		//Add a value before the modifiers are applied
		EventManager.AddBaseValueEvent += AddBasevalue;

		//Add to the modifier
		EventManager.AddModifierEvent += AddModifier;

		airPMod = soilPMod = waterPMod = landUMod = biodDMod = curMod =  popMod = 1.0f;
		airP = soilP = waterP = landU = bioD = cur = pop = new ResourceMessage();

		airP.Initialise(Resources.airPollution, initAirP, true);
		soilP.Initialise(Resources.soilPollution, initSoilP, true);
		waterP.Initialise(Resources.waterPollution, initWaterP, true);
		landU.Initialise(Resources.landUse, initLandU, true);
		bioD.Initialise(Resources.biodiversity, initBiodD, true);
		cur.Initialise(Resources.currency, initCur, true);
		pop.Initialise(Resources.population, initPop, true);

	}

	#region EventBased resources
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
	#endregion

	[SerializeField]
	private float airPMod, soilPMod, waterPMod, landUMod, biodDMod, curMod, popMod;
	[SerializeField]
	private int initAirP, initSoilP, initWaterP, initLandU, initBiodD, initCur, initPop;
	private ResourceMessage airP, soilP, waterP, landU, bioD, cur, pop;

	public void DailyResources() {
		airP.Initialise(Resources.airPollution, (int)(airP.amount * airPMod), true);
		soilP.Initialise(Resources.soilPollution, (int)(soilP.amount * soilPMod), true);
		waterP.Initialise(Resources.waterPollution, (int)(waterP.amount * waterPMod), true);
		landU.Initialise(Resources.landUse, (int)(landU.amount * landUMod), true);
		bioD.Initialise(Resources.biodiversity, (int)(bioD.amount * biodDMod), true);
		cur.Initialise(Resources.currency, (int)(cur.amount * curMod), true);
		pop.Initialise(Resources.population, (int)(pop.amount * popMod), true);

		EventManager._SendResourceMessage(airP, soilP, waterP, landU, bioD, cur, pop);
	}

	public void AddBasevalue(Resources res, int amt) {
		switch(res) {
			case Resources.airPollution:
				airP.amount += amt;
				break;
			case Resources.soilPollution:
				soilP.amount += amt;
				break;
			case Resources.waterPollution:
				waterP.amount += amt;
				break;
			case Resources.landUse:
				landU.amount += amt;
				break;
			case Resources.biodiversity:
				bioD.amount += amt;
				break;
			case Resources.currency:
				cur.amount += amt;
				break;
			case Resources.population:
				pop.amount += amt;
				break;
		}
	}

	public void AddModifier(Resources res, float amt) {
		switch(res) {
			case Resources.airPollution:
				airPMod += amt;
				break;
			case Resources.soilPollution:
				soilPMod += amt;
				break;
			case Resources.waterPollution:
				waterPMod += amt;
				break;
			case Resources.landUse:
				landUMod += amt;
				break;
			case Resources.biodiversity:
				biodDMod += amt;
				break;
			case Resources.currency:
				curMod += amt;
				break;
			case Resources.population:
				popMod += amt;
				break;
		}
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