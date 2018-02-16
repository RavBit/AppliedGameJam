using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script enables all animations within the animator which is attached to this script. 
//It subscribes to the thumbs up/down button events which are en-/disabled within the choiceManager.
[RequireComponent(typeof(Animator))]
public class GeneralAnimationController : MonoBehaviour {

	private Animator anim;

	private void Awake() {
		anim = GetComponent<Animator>();
	}

	private void OnEnable() {
		EventManager.ChoosePositive += Yes;
		EventManager.ChooseNegative += No;
	}

	private void OnDisable() {
		EventManager.ChoosePositive -= Yes;
		EventManager.ChooseNegative -= No;
	}

	private void OnDestroy() {
		EventManager.ChoosePositive -= Yes;
		EventManager.ChooseNegative -= No;
	}

	private void Yes() {
		anim.SetTrigger("Yes");
	}

	private void No() {
		anim.SetTrigger("No");
	}


}
