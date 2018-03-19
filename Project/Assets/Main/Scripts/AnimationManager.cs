using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Resourcrewrewes { airPollution, soilPollution, waterPollution, landUse, biodiversity, currency, population}
public enum Characters { alien, cook, farmer, geekyGirl, hotdog, jorji, lightBulb, octoGirl, protester, queenKing, rabbit, salesman, sheep, snake, teenager}

public class AnimationManager : MonoBehaviour {

	public GameObject alien, cook, farmer, geekyGirl, hotdog, jorji, lightBulb, octoGirl, protester, queenKing, rabbit, salesman, sheep, snake, teenager;

	void Start () {
		EventManager._GetAnim += GetChar;
	}
	
	public GameObject GetChar(Characters c) {
		GameObject character;
		switch(c) {
			case Characters.alien:
				character = alien;
				break;
			case Characters.cook:
				character = cook;
				break;
			case Characters.farmer:
				character = farmer;
				break;
			case Characters.geekyGirl:
				character = geekyGirl;
				break;
			case Characters.hotdog:
				character = hotdog;
				break;
			case Characters.jorji:
				character = jorji;
				break;
			case Characters.lightBulb:
				character = lightBulb;
				break;
			case Characters.octoGirl:
				character = octoGirl;
				break;
			case Characters.protester:
				character = protester;
				break;
			case Characters.queenKing:
				character = queenKing;
				break;
			case Characters.rabbit:
				character = rabbit;
				break;
			case Characters.salesman:
				character = salesman;
				break;
			case Characters.sheep:
				character = sheep;
				break;
			case Characters.snake:
				character = snake;
				break;
			case Characters.teenager:
				character = teenager;
				break;
			default:
				character = null;
				break;
		}
		return character;
	}
}
