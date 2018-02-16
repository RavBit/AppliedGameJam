using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//This script handles the tutorial sequence. It's hacky, I know.
public class Tutorial : MonoBehaviour {

	public int progression = 0;

	public GameObject[] tutorialElements;

	private void Start() {
		

		foreach(GameObject g in tutorialElements) {
			g.SetActive(false);
		}
		tutorialElements[0].SetActive(true);
	}

	public void Advance() {
		tutorialElements[progression].SetActive(false);
		progression++;
		if(progression < tutorialElements.Length - 1)
			tutorialElements[progression].SetActive(true);
		else
			SceneManager.LoadScene("Main");
	}
}
