using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppManager : MonoBehaviour {
    public static AppManager instance;
    public User User;
    public ResourceStorage RM;
    [Header("Username of logged in User")]
    [SerializeField]
    private string username;

    // Makes sure the App_Manager does not get destroyed & Singleton 
    void Awake()
    {
        if (instance != null)
            Debug.LogError("More than one App Manager in the scene");
        else
            instance = this;
        DontDestroyOnLoad(transform.gameObject);
        EventManager.SendV4 += ResourceUpdate;
    }


    public void SetUser(User user)
    {
        User = user;
    }
    public void ParseTowardsResources()
    {
        RM = new ResourceStorage();
        RM.airPollution = User.air_pollution;
        RM.soilPollution = User.soil_pollution;
        RM.waterPollution = User.water_pollution;
        RM.landUse = User.land_use;
        RM.biodiversity = User.biodiversity;
        RM.currency = User.currency;
        RM.population = User.population;
        RM.currency = User.currency;
        EventManager._SetupResourceManager(RM);
    }
    public void ResourceUpdate(ResourceStorage _RM)
    {
        RM = _RM;
    }
    public IEnumerator UpdateResources()
    {
        //Assigning strings from the text

        //Init form and give them the email and password
        WWWForm form = new WWWForm();
        form.AddField("user_ID", User.ID);
        form.AddField("air_pollution", RM.airPollution);
        form.AddField("soil_pollution", RM.soilPollution);
        form.AddField("water_pollution", RM.waterPollution);
        form.AddField("land_use", RM.landUse);
        form.AddField("biodiversity", RM.biodiversity);
        form.AddField("currency", RM.currency);
        form.AddField("population", RM.population);


        //Login to the website and wait for a response
        WWW w = new WWW("http://81.169.177.181/OLP/update_resources.php", form);
        yield return w;

        //Check if the response if empty or not
        if (string.IsNullOrEmpty(w.error))
        {
            Debug.Log("w.text: " + w.text);
            //Return the json array and put it in the C# User class
            Check check = JsonUtility.FromJson<Check>(w.text);
            if (check.success == true)
            {
                //Check if there is any error in the class. If there is return the error
                if (check.error != "")
                {
                    Debug.LogError("An error occured.. " + check.error);
                }
                else
                {
                    //Login the user and redirect it to a new scene
                    Debug.Log("Succes!");
                }
                //If the JsonArary is empty return this string in the feedback
            }
            else
            {
                Debug.LogError("An error occured.");
            }

            //If the string is empty return this string in the feedback
        }
        else
        {
            // error
            Debug.LogError("An error occured.");
        }

    }
}
public class Check
{
   public bool success;
   public  string error;
}