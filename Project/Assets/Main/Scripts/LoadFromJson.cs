using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class LoadFromJson : MonoBehaviour {

	[SerializeField]
	private string dataPath;
	[SerializeField]
	private ChoicesContainer choices;

	private void Awake() {
		dataPath = Application.streamingAssetsPath + "/Choices.json";
	}

	[ContextMenu("LoadFromJson")]
	private void LoadJson() {
		using(StreamReader stream = new StreamReader(dataPath)) {
			string json = stream.ReadToEnd();
			choices = JsonConvert.DeserializeObject<ChoicesContainer>(json);
			
			//choices = JsonUtility.FromJson<ChoicesContainer>(json);
			//Debug.Log($"Length of choices: {choices.choices.Length}");
		}
	}
}
