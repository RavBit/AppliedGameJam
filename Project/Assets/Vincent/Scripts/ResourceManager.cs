using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Resources { population, currency, happiness, environment}

public class ResourceManager : MonoBehaviour {

	private int population, currency, happiness, environment;

	//Properties for the four resources to enable function calls upon change. 

	/*
	 * ///////////////////////////////////////////////////////////////////////////////////
	 * ///																			   ///
	 * !!! The only place in this script that needs to be changed are these properties.!!!
	 * ///																			   ///
	 * ///////////////////////////////////////////////////////////////////////////////////
	 */

	private int Population {
		get {
			return population;
		}
		set {
			population += value;
			Debug.Log("Adding to population in property!");
			//Some other update functions that need to be called upon changing this value.
		}
	}
	private int Currency {
		get {
			return currency;
		}
		set {
			currency += value;
			Debug.Log("Adding to currency in property!");
			//Some other update functions that need to be called upon changing this value.
		}
	}
	private int Happiness {
		get {
			return happiness;
		}
		set {
			happiness += value;
			Debug.Log("Adding to happiness in property!" + value);
			//Some other update functions that need to be called upon changing this value.
		}
	}
	private int Environment {
		get {
			return environment;
		}
		set {
			environment += value;
			Debug.Log("Adding to environment in property!");
			//Some other update functions that need to be called upon changing this value.
		}
	}

	private void Start() {
		EventManager.SendResourceMessage += SendResourceMessage;
		//SendResourceMessage(new ResourceMessage(Resources.currency, 10), new ResourceMessage(Resources.happiness, 50), new ResourceMessage(Resources.environment, 30), new ResourceMessage(Resources.population, 100));
	}

	//This function is made to handle all resource change subjects. It accepts both positive and negative values.
	//It also accepts an infinite amount of changes per function call.
	//This is the main communication reciever meant for the rest of the game to have influence on the resources.
	public void SendResourceMessage(params ResourceMessage[] res) {
		if(res != null) {
			foreach(ResourceMessage i in res) {
				Resources temp = i.GetResourceType();
				//Debug.Log(temp);
				//Debug.Log(i.GetResourceType());
				switch(temp) {
					case Resources.population:
						Population += i.amount;
						break;
					case Resources.currency:
						Currency += i.amount;
						break;
					case Resources.happiness:
						Happiness += i.amount;
						break;
					case Resources.environment:
						Environment += i.amount;
						break;
					default:
						Debug.Log("Unhandled enum type");
						break;
				}
			}
		}
	}
}

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
