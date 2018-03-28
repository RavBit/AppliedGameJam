using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialButton : MonoBehaviour {

	public Button playbutton;
	public int delay = 10;

	private void Start() {
		StartCoroutine(ButtonEnable());
	}

	private IEnumerator ButtonEnable() {
		playbutton.interactable = false;
		yield return new WaitForSeconds(delay);
		playbutton.interactable = true;
	}
}
