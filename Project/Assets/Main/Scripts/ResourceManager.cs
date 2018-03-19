using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Resources { airPollution, soilPollution, waterPollution, landUse, biodiversity, currency, population}

public class ResourceManager : MonoBehaviour {

	private int airPollution, soilPollution, waterPollution, landUse, biodiversity, currency, population;

	private int prevAirPollution, prevSoilPollution, prevWaterPollution, prevLandUse, prevBiodiversity, prevCurrency, prevPopulation;

	private ResourceStorage resourceDelta = new ResourceStorage(0,0,0,0,0,0,0);

	#region resourceProperties
	private int AirPollution {
		get { return airPollution; }
		set { airPollution = value;
			airPollution = (int)Mathf.Clamp(airPollution, 0, 100); }
	}
	private int SoilPollution {
		get { return soilPollution; }
		set { soilPollution = value;
			soilPollution = (int)Mathf.Clamp(soilPollution, 0, 100); }
	}
	private int WaterPollution {
		get { return waterPollution; }
		set { waterPollution = value;
			waterPollution = (int)Mathf.Clamp(waterPollution, 0, 100); }
	}
	private int LandUse {
		get { return landUse; }
		set { landUse = value;
			landUse = (int)Mathf.Clamp(landUse, 0, 100); }
	}
	private int Biodiversity {
		get { return biodiversity; }
		set { biodiversity = value;
			biodiversity = (int)Mathf.Clamp(biodiversity, 0, 100); }
	}

	private int Population {
		get { return population; }
		set { population = value;}
	}
	private int Currency {
		get { return currency; }
		set { currency = value;}
	}
	#endregion

	private void Awake() {
		EventManager.SetupResources += SetupManager;
	}

	//Initialises the script
	private void Start() {
		EventManager.SendResourceMessage += SendResourceMessage;
		
	#region Resource event subscriptions
		EventManager.GetAirPollution += GetAirPollution;
		EventManager.GetSoilPollution += GetSoilPollution;
		EventManager.GetWaterPollution += GetWaterPollution;
		EventManager.GetLandUse += GetLandUse;
		EventManager.GetBiodiversity += GetBiodiversity;
		EventManager.GetPopulation += GetPopulation;
		EventManager.GetCurrency += GetCurrency;
	#endregion
	}

	//This function is made to handle all resource change subjects. It accepts both positive and negative values.
	//It also accepts an infinite amount of changes per function call.
	//This is the main communication reciever meant for the rest of the game to have influence on the resources.
	public void SendResourceMessage(params ResourceMessage[] res) {
		if(res != null) {
			SavePrevious();
			foreach(ResourceMessage i in res) {
				Resources temp = i.GetResourceType();
				int amt = i.Amount;
				switch(temp) {
					case Resources.airPollution:
						AirPollution = AirPollution + amt;
						break;
					case Resources.soilPollution:
						SoilPollution = SoilPollution + amt;
						break;
					case Resources.waterPollution:
						WaterPollution = WaterPollution + amt;
						break;
					case Resources.landUse:
						LandUse = LandUse + amt;
						break;
					case Resources.biodiversity:
						Biodiversity = Biodiversity + amt;
						break;
					case Resources.currency:
						Currency = Currency + amt;
						break;
					case Resources.population:
						Population = Population + amt;
						break;
					default:
						break;
				}
				if(i.lr != null) {
					EventManager._AddLivingResource(i.lr);
				}
				/*
				if(i.lrBaseChange != 0) {
					EventManager._AddBaseValue(i.resourceType, i.lrBaseChange);
				}
				if(i.lrModChange != 0) {
					EventManager._AddModifierValue(i.resourceType, i.lrModChange);
				}
				if(i.lr.breakingNews != "" || i.lr.breakingNews.Length < 1) {
					EventManager._SendBreakingNews(i.lr.breakingNews);
				}
				*/
			}
			EventManager._SendV4(CalculateDeltas());
			CheckEnd();
		}
	}

	//Saves the previous state the resources
	private void SavePrevious() {
		prevAirPollution = airPollution;
		prevSoilPollution = soilPollution;
		prevWaterPollution = waterPollution;
		prevLandUse = landUse;
		prevBiodiversity = biodiversity;
		prevCurrency = currency;
		prevPopulation = population;
	}

	//Checks end-conditions. Subject to change
	private void CheckEnd() {
		Debug.Log("EndCheck");
		if(airPollution >= 100) {
			EventManager._EndGame(Resources.airPollution);
		}
		if(soilPollution >= 100) {
			EventManager._EndGame(Resources.soilPollution);
		}
		if(waterPollution >= 100) {
			EventManager._EndGame(Resources.waterPollution);
		}
		if(landUse >= 100) {
			EventManager._EndGame(Resources.landUse);
		}
		if(biodiversity <= 0) {
			EventManager._EndGame(Resources.biodiversity);
		}
		if(currency <= -1000000) {
			EventManager._EndGame(Resources.currency);
		}
		if(population <= 0) {
			EventManager._EndGame(Resources.population);
		}		
	}

	//Used in the event to relay the storage struct to the ui manager
	private ResourceStorage CalculateDeltas() {
		resourceDelta = new ResourceStorage(airPollution, waterPollution, soilPollution , landUse, biodiversity, currency , population);
		return resourceDelta;
	}
	
	#region private Getters
	private int GetAirPollution() {
		return airPollution;
	}
	private int GetSoilPollution() {
		return soilPollution; 
	}
	private int GetWaterPollution() {
		return waterPollution; 
	}
	private int GetLandUse() {
		return landUse; 
	}
	private int GetBiodiversity() {
		return biodiversity; 
	}
	private int GetPopulation() { 
		return population; 
	}
	private int GetCurrency() {
		return currency; 
	}
	#endregion

	public void SetupManager(ResourceStorage rs) {
		airPollution = rs.airPollution;
		waterPollution = rs.waterPollution;
		soilPollution = rs.soilPollution;
		landUse = rs.landUse;
		biodiversity = rs.biodiversity;
		currency = rs.currency;
		population = rs.population;
		SavePrevious();
	}
}

//This struct is used to relay the resources to the UIManager
public struct ResourceStorage {
	public ResourceStorage(int ap, int wp, int sp, int lu, int bd, int cur, int pop) {
		airPollution = ap;
		waterPollution = wp;
		soilPollution = sp;
		landUse = lu;
		biodiversity = bd;
		currency = cur;
		population = pop;

		//Average for now.
		pollution = (airPollution + waterPollution + soilPollution) / 3f;
	}
	public float pollution;
	public int airPollution, waterPollution, soilPollution;

	public int landUse, biodiversity;

	public int currency, population;

}
