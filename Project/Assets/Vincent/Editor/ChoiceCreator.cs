using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ChoiceCreator : EditorWindow {

	private string fileName = "DefaultName";
	public ResourceMessage message;
	private List<ResourceMessage> negMessages;
	private List<ResourceMessage> posMessages;
	public Choice choice;
	public Dialog negative;
	public Dialog positive;


	public int[] ints = {0, 1, 2};

	[MenuItem("Window/Choice Creator")]
	static public void OpenWindow() {
		ChoiceCreator window = (ChoiceCreator)GetWindow(typeof(ChoiceCreator));
		window.minSize = new Vector2(400, 350);
		window.maxSize = new Vector2(400, 400);
		window.Show();
	}

	private void OnEnable() {
		InitData();
	}

	private void PositiveField() {
		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Positive dialog");
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Dialog");
		positive.text = EditorGUILayout.TextField(positive.text, GUILayout.Height(80));
		EditorGUILayout.EndHorizontal();
	}

	private void NegativeField() {
		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Negative dialog");
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Dialog");
		negative.text = EditorGUILayout.TextField(negative.text, GUILayout.Height(80));
		EditorGUILayout.EndHorizontal();
	}

	private void OnGUI() {
		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("FileName");
		fileName = EditorGUILayout.TextField(fileName);
		EditorGUILayout.EndHorizontal();

		PositiveField();
		NegativeField();

		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Message to add");
		message = EditorGUILayout.ObjectField(message, typeof(ResourceMessage), false) as ResourceMessage;
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.BeginHorizontal();
		if(message != null) {
			if(GUILayout.Button("Push Message to positive list", GUILayout.Height(30))) {
				posMessages.Add(message);
				message = null;
			}
			if(GUILayout.Button("Push Message to negative list", GUILayout.Height(30))) {
				negMessages.Add(message);
				message = null;
			}
		}
		else {
			EditorGUILayout.HelpBox("No message to add.", MessageType.Warning);
		}
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		if(GUILayout.Button("Reset all values", GUILayout.Height(30))) {
			InitData();
		}
		if(fileName == null || fileName.Length < 1 || fileName == "DefaultName") {
			EditorGUILayout.HelpBox("FileName is too short or hasn't been changed.", MessageType.Warning);
		}
		else if(GUILayout.Button("Create Choice", GUILayout.Height(30))) {
			SaveChoice();
		}
		EditorGUILayout.EndHorizontal();
	}

	public void InitData() {
		choice = new Choice();
		negative = new Dialog();
		positive = new Dialog();
	}

	private void SaveChoice() {
		negative.messages = new ResourceMessage[negMessages.Count];
		positive.messages = new ResourceMessage[posMessages.Count];
		for(int i = 0; i < negMessages.Count; i++) {
			negative.messages[i] = negMessages[i];
		}
		for(int i = 0; i < posMessages.Count; i++) {
			positive.messages[i] = posMessages[i];
		}
		choice.NegativeDialog = negative;
		choice.PositiveDialog = positive;

		string dataPath = "Assets/Resources/DialogOptions/NegativeDialog/" + fileName + ".asset";
		AssetDatabase.CreateAsset(negative, dataPath);
		dataPath = "Assets/Resources/DialogOptions/PositiveDialog/" + fileName + ".asset";
		AssetDatabase.CreateAsset(positive, dataPath);
		dataPath = "Assets/Resources/DialogOptions/Choices/" + fileName + ".asset";
		AssetDatabase.CreateAsset(choice, dataPath);
		InitData();
	}
}
