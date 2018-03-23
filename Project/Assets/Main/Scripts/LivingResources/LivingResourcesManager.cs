using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LivingResourcesManager : MonoBehaviour {

	[SerializeField]
	private float airPMod, soilPMod, waterPMod, landUMod, biodDMod, curMod, popMod;
	[SerializeField]
	private int initAirP, initSoilP, initWaterP, initLandU, initBiodD, initCur, initPop;
	private ResourceMessage airP, soilP, waterP, landU, bioD, cur, pop;

	[SerializeField]
	private List<LivingResource> livingResources;

	private void Awake() {
		livingResources = new List<LivingResource>();
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

		airP.Initialise(Resources.airPollution, initAirP);
		soilP.Initialise(Resources.soilPollution, initSoilP);
		waterP.Initialise(Resources.waterPollution, initWaterP);
		landU.Initialise(Resources.landUse, initLandU);
		bioD.Initialise(Resources.biodiversity, initBiodD);
		cur.Initialise(Resources.currency, initCur);
		pop.Initialise(Resources.population, initPop);

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
		bool canAdd;
		if(res != null) {
			foreach(LivingResource i in res) {
				canAdd = true;
				foreach(LivingResource r in livingResources) {
					if(i == r)
						canAdd = false;
				}
				if(canAdd)
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

	

	public void DailyResources() {
		airP.Initialise(Resources.airPollution, (int)(airP.amount * airPMod));
		soilP.Initialise(Resources.soilPollution, (int)(soilP.amount * soilPMod));
		waterP.Initialise(Resources.waterPollution, (int)(waterP.amount * waterPMod));
		landU.Initialise(Resources.landUse, (int)(landU.amount * landUMod));
		bioD.Initialise(Resources.biodiversity, (int)(bioD.amount * biodDMod));
		cur.Initialise(Resources.currency, (int)(cur.amount * curMod));
		pop.Initialise(Resources.population, (int)(pop.amount * popMod));

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


