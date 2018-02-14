using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ResourceMessageCreator : EditorWindow {

	private ResourceMessage res;
	private Resources resources = Resources.currency;
	private bool isToday = true;
	private int amount = 0;

	[MenuItem("Window/Message Creator")]
	static public void OpenWindow() {
		ResourceMessageCreator window = (ResourceMessageCreator)GetWindow(typeof(ResourceMessageCreator));
		window.minSize = new Vector2(250, 150);
		window.maxSize = new Vector2(250, 150);
		window.Show();
	}

	private void OnEnable() {
		InitData();
	}

	private void OnGUI() {
		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Resource Message Creator");
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Resource Type");
		res.resourceType = (Resources)EditorGUILayout.EnumPopup(res.resourceType);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Happens this day?");
		res.isToday = (bool)EditorGUILayout.Toggle(res.isToday);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Amount (can be negative)");
		res.amount = (int)EditorGUILayout.FloatField(res.amount);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		if(GUILayout.Button("Reset all values", GUILayout.Height(30))) {
			InitData();
		}
		if(GUILayout.Button("Create Message", GUILayout.Height(30))) {
			SaveMessage();
		}
		EditorGUILayout.EndHorizontal();
	}


	public void InitData() {
		res = new ResourceMessage(Resources.currency, 0, true);
	}

	private void SaveMessage() {
		string typeString = "";
		switch(res.resourceType) {
			case Resources.population:
				if(res.isToday)
					typeString = "Population" + res.amount + "Today";
				else
					typeString = "Population" + res.amount + "NotToday";
				break;
			case Resources.currency:
				if(res.isToday)
					typeString = "Currency" + res.amount + "Today";
				else
					typeString = "Currency" + res.amount + "NotToday";
				break;
			case Resources.happiness:
				if(res.isToday)
					typeString = "Happiness" + res.amount + "Today";
				else
					typeString = "Happiness" + res.amount + "NotToday";
				break;
			case Resources.environment:
				if(res.isToday)
					typeString = "Environment" + res.amount + "Today";
				else
					typeString = "Environment" + res.amount + "NotToday";
				break;
		}
		string dataPath = "Assets/Resources/DialogOptions/ResourceMessage/" + typeString + ".asset";
		AssetDatabase.CreateAsset(res, dataPath);
		InitData();
	}
}
