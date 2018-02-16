using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Resources { population, currency, happiness, environment}

public class ResourceManager : MonoBehaviour {

	private int population = 10000;
	private int currency = 1000000;
	private int happiness = 70;
	private int environment = 85;

	private int prevPop = 10000;
	private int prevCur = 1000000;
	private int prevHap = 70;
	private int prevEnv = 85;

	private ResourceStorage resourceDelta = new ResourceStorage(0,0,0,0);

	private int Population {
		get {
			return population;
		}
		set {
			population = value;
			//Some other update functions that need to be called upon changing this value.
		}
	}
	private int Currency {
		get {
			return currency;
		}
		set {
			currency = value;
			//Some other update functions that need to be called upon changing this value.
		}
	}
	private int Happiness {
		get {
			return happiness;
		}
		set {
			happiness =  value;
			//Some other update functions that need to be called upon changing this value.
		}
	}
	private int Environment {
		get {
			return environment;
		}
		set {
			environment = value;
			//Some other update functions that need to be called upon changing this value.
		}
	}
	
	//Initialises the script
    private void Start() {
		EventManager.SendResourceMessage += SendResourceMessage;
        EventManager.GetPopulation += Get_Population;
        EventManager.GetHappiness += GetHappiness;
        EventManager.GetEnvironment += GetEnvironment;
        EventManager.GetCurrency += GetCurrency;
	}

	//This function is made to handle all resource change subjects. It accepts both positive and negative values.
	//It also accepts an infinite amount of changes per function call.
	//This is the main communication reciever meant for the rest of the game to have influence on the resources.
	public void SendResourceMessage(params ResourceMessage[] res) {
		if(res != null) {
			SavePrevious();
			foreach(ResourceMessage i in res) {
				Resources temp = i.GetResourceType();
				int amt = i.amount;
				switch(temp) {
					case Resources.population:
						Population = Population + amt;
						break;
					case Resources.currency:
						Currency = Currency + amt;
						break;
					case Resources.happiness:
						Happiness = Happiness + amt;
						break;
					case Resources.environment:
						Environment = Environment + amt;
						break;
					default:
						Debug.Log("Unhandled enum type");
						break;
				}
			}
			EventManager._SendV4(CalculateDeltas());
			CheckEnd();
		}
	}

	//Saves the previous state the resources
	private void SavePrevious() {
		prevPop = population;
		prevCur = currency;
		prevHap = happiness;
		prevEnv = environment;
	}

	//Checks end-conditions. Subject to change
	private void CheckEnd() {
		Debug.Log("EndCheck");
		if(population <= 0) {
			EventManager._EndGame(Resources.population);
		}
		if(currency <= -1000000) {
			EventManager._EndGame(Resources.currency);
		}
		if(happiness <= 0) {
			EventManager._EndGame(Resources.happiness);
		}
		if(environment <= 0) {
			EventManager._EndGame(Resources.environment);
		}
	}

	//Used in the event to relay the storage struct to the ui manager
	private ResourceStorage CalculateDeltas() {
		resourceDelta = new ResourceStorage(population - prevPop, currency - prevCur, happiness - prevHap, environment - prevEnv);
		return resourceDelta;
	}

	private int GetHappiness() {
		return happiness;
	}
	private int GetCurrency() {
		return currency;
	}
	private int GetEnvironment() {
		return environment;
	}
	private int Get_Population() {
		return population;
	}
}

//This struct is used to relay the resources to the UIManager
public struct ResourceStorage {
	public ResourceStorage(int pop, int cur, int hap, int env) {
		population = pop;
		currency = cur;
		happiness = hap;
		environment = env;
	}
	public int population, currency, happiness, environment;
}
