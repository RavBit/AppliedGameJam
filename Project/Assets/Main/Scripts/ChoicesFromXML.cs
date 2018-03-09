using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System;

public class ChoicesFromXML {

	public ChoiceDataContainer choiceDataList;

	public void Start() {
		LoadData();
	}

	public void ChangeReflectionToChoice() {
		foreach(ChoiceReflection cr in choiceDataList.choices) {
			Choice choice = new Choice();

			Dialog dialogPos = new Dialog();
			ResourceMessage[] messagesPos = new ResourceMessage[7];

			Dialog dialogNeg = new Dialog();
			ResourceMessage[] messagesNeg = new ResourceMessage[7];

			dialogPos.text = cr.textPositive;
			messagesPos[0].Initialise(Resources.airPollution, cr.airPollutionPos, true);
			messagesPos[1].Initialise(Resources.soilPollution, cr.soilPollutionPos, true);
			messagesPos[2].Initialise(Resources.waterPollution, cr.waterPollutionPos, true);
			messagesPos[3].Initialise(Resources.landUse, cr.landUsePos, true);
			messagesPos[4].Initialise(Resources.biodiversity, cr.biodiversityPos, true);
			messagesPos[5].Initialise(Resources.currency, cr.currencyPos, true);
			messagesPos[6].Initialise(Resources.population, cr.populationPos, true);
			dialogPos.messages = messagesPos;

			dialogNeg.text = cr.textNegative;
			messagesNeg[0].Initialise(Resources.airPollution, cr.airPollutionNeg, true);
			messagesNeg[1].Initialise(Resources.soilPollution, cr.soilPollutionNeg, true);
			messagesNeg[2].Initialise(Resources.waterPollution, cr.waterPollutionNeg, true);
			messagesNeg[3].Initialise(Resources.landUse, cr.landUseNeg, true);
			messagesNeg[4].Initialise(Resources.biodiversity, cr.biodiversityNeg, true);
			messagesNeg[5].Initialise(Resources.currency, cr.currencyNeg, true);
			messagesNeg[6].Initialise(Resources.population, cr.populationNeg, true);
			dialogNeg.messages = messagesNeg;

			choice.Name = cr.characterName;
			choice.Dilemma = cr.choiceText;
			choice.State = State.Neutral;
			choice.character = GetCharFromInt(cr.characterSprite);
			choice.PositiveDialog = dialogPos;
			choice.NegativeDialog = dialogNeg;

		}
	}

	private Characters GetCharFromInt(int num) {
		switch(num) {
			case 0:
				return Characters.alien;
			case 1:
				return Characters.cook;
			case 2:
				return Characters.farmer;
			case 3:
				return Characters.geekyGirl;
			case 4:
				return Characters.hotdog;
			case 5:
				return Characters.jorji;
			case 6:
				return Characters.lightBulb;
			case 7:
				return Characters.octoGirl;
			case 8:
				return Characters.protester;
			case 9:
				return Characters.queenKing;
			case 10:
				return Characters.rabbit;
			case 11:
				return Characters.salesman;
			case 12:
				return Characters.sheep;
			case 13:
				return Characters.snake;
			case 14:
				return Characters.teenager;
			default:
				return Characters.alien;
		}
	}

	public void LoadData() {
		XmlSerializer serializer = new XmlSerializer(typeof(ChoiceDataContainer));
		FileStream stream = new FileStream(Application.streamingAssetsPath + "/Choices_XML.xml", FileMode.Open);
		choiceDataList = serializer.Deserialize(stream) as ChoiceDataContainer;
		stream.Close();
	}
}

[Serializable]
public class ChoiceDataContainer {
	[XmlArray("ChoiceDataArray")]
	[XmlArrayItem("ChoiceReflection")]
	public List<ChoiceReflection> choices = new List<ChoiceReflection>();
}

public class ChoiceReflection {
	[XmlAttribute("CharacterName")]
	public string characterName;
	public string fileName;
	public int characterSprite;
	public string choiceText;

	//[XmlAttribute("Positive Dialog")]
	public string textPositive;
	//[XmlAttribute("ResourceChanges")]
	public int airPollutionPos;
	public int soilPollutionPos;
	public int waterPollutionPos;
	public int landUsePos;
	public int biodiversityPos;
	public int currencyPos;
	public int populationPos;

	//[XmlAttribute("Negative Dialog")]
	public string textNegative;
	//[XmlAttribute("ResourceChanges")]
	public int airPollutionNeg;
	public int soilPollutionNeg;
	public int waterPollutionNeg;
	public int landUseNeg;
	public int biodiversityNeg;
	public int currencyNeg;
	public int populationNeg;
}