using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToSite : MonoBehaviour {

	public void LoadSite(string url) {
		Application.OpenURL(url);
	}
}
