using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class ResourceMessageCreator : EditorWindow {

	private bool isPositive = true;
	private ResourceMessage airPol,	waterPol, soilPol, landUse,	bioD, cur, pop;
	private string nameHost = "";
	private string posNegString = "";
	private Resources resources = Resources.currency;
	private bool isToday = true;
	private int amount = 0;

	[MenuItem("Window/Message Creator")]
	static public void OpenWindow() {
		ResourceMessageCreator window = (ResourceMessageCreator)GetWindow(typeof(ResourceMessageCreator));
		window.minSize = new Vector2(400, 680);
		window.maxSize = new Vector2(400, 680);
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

		DisplayMessage(airPol);
		DisplayMessage(waterPol);
		DisplayMessage(soilPol);
		DisplayMessage(landUse);
		DisplayMessage(bioD);
		DisplayMessage(cur);
		DisplayMessage(pop);

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
			case Resources.airPollution:
				typeResource = "AirPollution";
				break;
			case Resources.soilPollution:
				typeResource = "SoilPollution";
				break;
			case Resources.waterPollution:
				typeResource = "WaterPollution";
				break;
			case Resources.landUse:
				typeResource = "LandUse";
				break;
			case Resources.biodiversity:
				typeResource = "Biodiversity";
				break;
			case Resources.currency:
				typeResource = "Currency";
				break;
			case Resources.population:
				typeResource = "Population";
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
		airPol = CreateInstance<ResourceMessage>();
		waterPol = CreateInstance<ResourceMessage>();
		soilPol = CreateInstance<ResourceMessage>();
		landUse = CreateInstance<ResourceMessage>();
		bioD = CreateInstance<ResourceMessage>();
		cur = CreateInstance<ResourceMessage>();
		pop = CreateInstance<ResourceMessage>();

		airPol.Initialise(Resources.airPollution, 0, true);
		waterPol.Initialise(Resources.waterPollution, 0, true);
		soilPol.Initialise(Resources.soilPollution, 0, true);
		landUse.Initialise(Resources.landUse, 0, true);
		bioD.Initialise(Resources.biodiversity, 0, true);
		cur.Initialise(Resources.currency, 0, true);
		pop.Initialise(Resources.population, 0, true);
	}

	private void SaveMessage() {
		string airString = "";
		string waterString = "";
		string soilString = "";
		string landString = "";
		string bioString = "";
		string curString = "";
		string popString = "";
		#region NameChecks
		if(airPol.isToday)
			airString = "AirPollution" + airPol.amount + "Today";
		else
			airString = "AirPollution" + airPol.amount + "NotToday";

		if(waterPol.isToday)
			waterString = "WaterPollution" + waterPol.amount + "Today";
		else
			waterString = "WaterPollution" + waterPol.amount + "NotToday";

		if(soilPol.isToday)
			soilString = "SoilPollution" + soilPol.amount + "Today";
		else
			soilString = "SoilPollution" + soilPol.amount + "NotToday";

		if(landUse.isToday)
			landString = "LandUse" + landUse.amount + "Today";
		else
			landString = "LandUse" + landUse.amount + "NotToday";

		if(bioD.isToday)
			bioString = "Biodiversity" + bioD.amount + "Today";
		else
			bioString = "Biodiversity" + bioD.amount + "NotToday";

		if(cur.isToday)
			curString = "Currency" + cur.amount + "Today";
		else
			curString = "Currency" + cur.amount + "NotToday";

		if(pop.isToday)
			popString = "Population" + pop.amount + "Today";
		else
			popString = "Population" + pop.amount + "NotToday";
#endregion

		if(isPositive)
			posNegString = "Positive";
		else
			posNegString = "Negative";

		if(!Directory.Exists("Assets/Resources/DialogOptions/ResourceMessage/" + nameHost))
			AssetDatabase.CreateFolder("Assets/Resources/DialogOptions/ResourceMessage", nameHost);

		if(!Directory.Exists("Assets/Resources/DialogOptions/ResourceMessage/" + nameHost + "/" + posNegString))
			AssetDatabase.CreateFolder("Assets/Resources/DialogOptions/ResourceMessage/" + nameHost, posNegString);

		string dataPathAir = "Assets/Resources/DialogOptions/ResourceMessage/" + nameHost + "/" + posNegString + "/" + airString + ".asset";
		string dataPathWater = "Assets/Resources/DialogOptions/ResourceMessage/" + nameHost + "/" + posNegString + "/" + waterString + ".asset";
		string dataPathSoil = "Assets/Resources/DialogOptions/ResourceMessage/" + nameHost + "/" + posNegString + "/" + soilString + ".asset";
		string dataPathLand = "Assets/Resources/DialogOptions/ResourceMessage/" + nameHost + "/" + posNegString + "/" + landString + ".asset";
		string dataPathBioD = "Assets/Resources/DialogOptions/ResourceMessage/" + nameHost + "/" + posNegString + "/" + bioString + ".asset";
		string dataPathCur = "Assets/Resources/DialogOptions/ResourceMessage/" + nameHost + "/" + posNegString + "/" + curString + ".asset";
		string dataPathPop = "Assets/Resources/DialogOptions/ResourceMessage/" + nameHost + "/" + posNegString + "/" + popString + ".asset";

		if(airPol.amount != 0)
			AssetDatabase.CreateAsset(airPol, dataPathAir);
		if(waterPol.amount != 0)
			AssetDatabase.CreateAsset(waterPol, dataPathWater);
		if(soilPol.amount != 0)
			AssetDatabase.CreateAsset(soilPol, dataPathSoil);
		if(landUse.amount != 0)
			AssetDatabase.CreateAsset(landUse, dataPathLand);
		if(bioD.amount != 0)
			AssetDatabase.CreateAsset(bioD, dataPathBioD);
		if(cur.amount != 0)
			AssetDatabase.CreateAsset(cur, dataPathCur);
		if(pop.amount != 0)
			AssetDatabase.CreateAsset(pop, dataPathPop);
		InitData();
	}
}
