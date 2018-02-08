using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
