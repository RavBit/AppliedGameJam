using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Characters { alien, cook, farmer, geekyGirl, hotdog, jorji, lightBulb, octoGirl, protester, queenKing, rabbit, salesman, sheep, snake, teenager}
public enum HealthState { healthy, fat, skinny, ill }

public class AnimationManager : MonoBehaviour {

	public ResourceStorage res;
	public HealthState hs;

	public GameObject[] healthy, ill, skinny, fat;

	void Start () {
		EventManager.SendV4 += CatchResources;
		EventManager._GetAnim += GetChar;
	}
	
	public GameObject GetChar(Characters c) {
		GameObject character;
		switch(c) {
			case Characters.alien:
				character = GetState(Characters.alien);
				break;
			case Characters.cook:
				character = GetState(Characters.cook);
				break;
			case Characters.farmer:
				character = GetState(Characters.farmer);
				break;
			case Characters.geekyGirl:
				character = GetState(Characters.geekyGirl);
				break;
			case Characters.hotdog:
				character = GetState(Characters.hotdog);
				break;
			case Characters.jorji:
				character = GetState(Characters.jorji);
				break;
			case Characters.lightBulb:
				character = GetState(Characters.lightBulb);
				break;
			case Characters.octoGirl:
				character = GetState(Characters.octoGirl);
				break;
			case Characters.protester:
				character = GetState(Characters.protester);
				break;
			case Characters.queenKing:
				character = GetState(Characters.queenKing);
				break;
			case Characters.rabbit:
				character = GetState(Characters.rabbit);
				break;
			case Characters.salesman:
				character = GetState(Characters.salesman);
				break;
			case Characters.sheep:
				character = GetState(Characters.sheep);
				break;
			case Characters.snake:
				character = GetState(Characters.snake);
				break;
			case Characters.teenager:
				character = GetState(Characters.teenager);
				break;
			default:
				character = null;
				break;
		}
		return character;
	}

	public GameObject GetState(Characters c) {
		GameObject character;
		switch(hs) {
			case HealthState.healthy:
				character = healthy[(int)c];
				break;
			case HealthState.fat:
				character = fat[(int)c];
				break;
			case HealthState.skinny:
				character = skinny[(int)c];
				break;
			case HealthState.ill:
				character = ill[(int)c];
				break;
			default:
				character = null;
				break;
		}
		return character;
	}

	public void CalcHealthState() {
		int pollution = Mathf.Clamp((res.airPollution + res.waterPollution + res.soilPollution)/ 3, 0, 100);
		if(pollution < 25) {
			hs = HealthState.fat;
		}
		else if(pollution >= 25 && pollution < 50) {
			hs = HealthState.healthy;
		}
		else if(pollution >= 50 && pollution < 75) {
			hs = HealthState.skinny;
		}
		else if(pollution > 75) {
			hs = HealthState.ill;
		}
		EventManager._UpdateHealth(hs);
	}

	public void CatchResources(ResourceStorage v4) {
		res = v4;
		CalcHealthState();
	}
}
