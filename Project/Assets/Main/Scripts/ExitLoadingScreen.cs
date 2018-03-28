using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitLoadingScreen : MonoBehaviour {

	public GameObject buttonQMark;

	private void OnDisable() {
		buttonQMark.SetActive(true);
	}
}
