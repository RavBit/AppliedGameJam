using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class ResourceMessageCreator : EditorWindow {

	private bool isPositive = true;
	private ResourceMessage pop;
	private ResourceMessage hap;
	private ResourceMessage cur;
	private ResourceMessage env;
	private string nameHost = "";
	private string posNegString = "";
	private Resources resources = Resources.currency;
	private bool isToday = true;
	private int amount = 0;

	[MenuItem("Window/Message Creator")]
	static public void OpenWindow() {
		ResourceMessageCreator window = (ResourceMessageCreator)GetWindow(typeof(ResourceMessageCreator));
		window.minSize = new Vector2(400, 390);
		window.maxSize = new Vector2(400, 390);
		window.Show();
	}

	private void OnEnable() {
		InitData();
	}

	private void OnGUI() {
		GUILayout.Label("Resource Message Creator", EditorStyles.boldLabel);
		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Name", EditorStyles.boldLabel);
		nameHost = (string)EditorGUILayout.TextField(nameHost);
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Dialog option is positive", EditorStyles.boldLabel);
		isPositive = (bool)EditorGUILayout.Toggle(isPositive);
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.Space();

		DisplayMessage(pop);
		DisplayMessage(cur);
		DisplayMessage(hap);
		DisplayMessage(env);

		EditorGUILayout.BeginHorizontal();
		if(GUILayout.Button("Reset all values", GUILayout.Height(30))) {
			InitData();
		}
		if(nameHost != null && nameHost.Length < 1) {
			EditorGUILayout.HelpBox("FileName is too short or hasn't been changed.", MessageType.Warning);
		}else if(GUILayout.Button("Create Message", GUILayout.Height(30))) {
			SaveMessage();
		}
		EditorGUILayout.EndHorizontal();
	}

	private void DisplayMessage(ResourceMessage res) {
		string typeResource;
		switch(res.resourceType) {
			case Resources.population:
				typeResource = "Population";
				break;
			case Resources.currency:
				typeResource = "Currency";
				break;
			case Resources.happiness:
				typeResource = "Happiness";
				break;
			case Resources.environment:
				typeResource = "Environment";
				break;
			default:
				typeResource = "";
				break;
		}
		EditorGUILayout.BeginHorizontal();
		GUILayout.Label(typeResource, EditorStyles.boldLabel);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Happens this day?");
		res.isToday = (bool)EditorGUILayout.Toggle(res.isToday);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Amount (can be negative)");
		res.amount = (int)EditorGUILayout.FloatField(res.amount);
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.Space();
	}


	public void InitData() {
		nameHost = "";
		isPositive = true;
		pop = new ResourceMessage(Resources.population, 0, true);
		hap = new ResourceMessage(Resources.happiness, 0, true);
		cur = new ResourceMessage(Resources.currency, 0, true);
		env = new ResourceMessage(Resources.environment, 0, true);
	}

	private void SaveMessage() {
		string curString = "";
		string popString = "";
		string hapString = "";
		string envString = "";

		if(pop.isToday)
			popString = "Population" + pop.amount + "Today";
		else
			popString = "Population" + pop.amount + "NotToday";

		if(cur.isToday)
			curString = "Currency" + cur.amount + "Today";
		else
			curString = "Currency" + cur.amount + "NotToday";

		if(hap.isToday)
			hapString = "Happiness" + hap.amount + "Today";
		else
			hapString = "Happiness" + hap.amount + "NotToday";

		if(env.isToday)
			envString = "Environment" + env.amount + "Today";
		else
			envString = "Environment" + env.amount + "NotToday";

		if(isPositive)
			posNegString = "Positive";
		else
			posNegString = "Negative";

		if(!Directory.Exists("Assets/Resources/DialogOptions/ResourceMessage/" + nameHost))
			AssetDatabase.CreateFolder("Assets/Resources/DialogOptions/ResourceMessage", nameHost);

		if(!Directory.Exists("Assets/Resources/DialogOptions/ResourceMessage/" + nameHost + "/" + posNegString))
			AssetDatabase.CreateFolder("Assets/Resources/DialogOptions/ResourceMessage/" + nameHost, posNegString);

		string dataPathCur = "Assets/Resources/DialogOptions/ResourceMessage/" + nameHost + "/" + posNegString + "/" + curString + ".asset";
		string dataPathPop = "Assets/Resources/DialogOptions/ResourceMessage/" + nameHost + "/" + posNegString + "/" + popString + ".asset";
		string dataPathHap = "Assets/Resources/DialogOptions/ResourceMessage/" + nameHost + "/" + posNegString + "/" + hapString + ".asset";
		string dataPathEnv = "Assets/Resources/DialogOptions/ResourceMessage/" + nameHost + "/" + posNegString + "/" + envString + ".asset";

		if(cur.amount != 0)
			AssetDatabase.CreateAsset(cur, dataPathCur);
		if(pop.amount != 0)
			AssetDatabase.CreateAsset(pop, dataPathPop);
		if(hap.amount != 0)
			AssetDatabase.CreateAsset(hap, dataPathHap);
		if(env.amount != 0)
			AssetDatabase.CreateAsset(env, dataPathEnv);
		InitData();
	}
}
