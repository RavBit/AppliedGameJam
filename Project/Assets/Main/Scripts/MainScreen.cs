using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//A simple LoadScene meant for a UI button.
public class MainScreen : MonoBehaviour {

	public void StartScene() {
		SceneManager.LoadScene("TutorialScene");
	}
}
