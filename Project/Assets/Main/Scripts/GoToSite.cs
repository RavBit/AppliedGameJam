using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToSite : MonoBehaviour {

	public void LoadSite(string url) {
		Application.OpenURL(url);
	}

    public void LoadStart()
    {
        SceneManager.LoadScene(0);
    }
}
