using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

//This script manages the placing and removal of choices in the gamescene.
public class ChoiceManager : MonoBehaviour {
    public List<Choice> Choices;
    public Choice curchoice;
    public int choicecounter = 0;
    public Queue<ResourceMessage> ChoiceQueue = new Queue<ResourceMessage>();

	[SerializeField]
	private string dataPath; //= Application.streamingAssetsPath + "/Choices.json";
	[SerializeField]
	private ChoicesContainer choices;

	private void Awake() {
        string url = "http://81.169.177.181/OLP/Choices.json";
        PlayerPrefs.DeleteAll();
        if(PlayerPrefs.HasKey("Choice") == false)
        {
            PlayerPrefs.SetInt("Choice", 0);
        }
        choicecounter = PlayerPrefs.GetInt("Choice");
        WWW www = new WWW(url);
        StartCoroutine(WaitForRequest(www));
    }
    IEnumerator WaitForRequest(WWW www)
    {
        yield return www;
        Debug.Log("www : " + www.text);
        LoadJson(www.text);
    }
    public int GetChoice()
    {
        return Choices.Count;
    }
    private void Start() {
        EventManager.ChoiceLoad += LoadChoice;
        EventManager.ChoosePositive += PositiveChoice;
        EventManager.ChooseNegative += NegativeChoice;
        EventManager.ChoiceUnLoad += UnLoadChoice;
        EventManager.GetQueue += Get_Queue;
        EventManager.NextDay += ResetQueue;
        EventManager.NightCycle += BeginNight;
        EventManager.ChoiceGet += GetChoice;
        Invoke("Initialize", .000001f);
    }
	private Queue<ResourceMessage> Get_Queue() {
        return ChoiceQueue;
    }

    private void ResetQueue() {
        ChoiceQueue = new Queue<ResourceMessage>();
    }

	private void BeginNight() {
        Invoke("EndNight", 3);
    }

	private void EndNight() {
        EventManager.InterMission_Continue();
    }
	private void Initialize() {
        Invoke("Init", 3);
	}
    private void Init()
    {
        EventManager.Choice_Load(Choices[choicecounter]);
    }
    private void PositiveChoice() {
        curchoice.State = State.Positive;
        EventManager.Display_Choice(curchoice);
    }
	private void NegativeChoice() {
        curchoice.State = State.Negative;
        EventManager.Display_Choice(curchoice);
    }
	private void LoadChoice(Choice _choice) {
        curchoice = _choice;
    }

	//This function removes the current choice from the gamescene and submits all the resourceMessages to the gameManager.
    private void UnLoadChoice() {
        choicecounter++;
        if (choicecounter == Choices.Count && Choices.Count != 0)
        {
			EventManager._GameOver();
        }
        PlayerPrefs.SetInt("Choice", choicecounter);
        if (curchoice.State == State.Positive) {
            foreach (ResourceMessage rm in curchoice.PositiveDialog.messages) {
                ChoiceQueue.Enqueue(rm);
            }
        }
        if (curchoice.State == State.Negative) {
            foreach (ResourceMessage rm in curchoice.NegativeDialog.messages) {
                ChoiceQueue.Enqueue(rm);
            }

        }
        EventManager.Choice_Load(Choices[choicecounter]);
    }

	[ContextMenu("LoadFromJson")]
	private void LoadJson(string json) {
        choices = JsonConvert.DeserializeObject<ChoicesContainer>(json);
        //choices = JsonUtility.FromJson<ChoicesContainer>(json);
        //Debug.Log($"Length of choices: {choices.choices.Length}");
		Choices = choices.choices.ToList();
        var rnd = new System.Random();
        Choices = Choices.OrderBy(x => Random.value).ToList();
    }
}
