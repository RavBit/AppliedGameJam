using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class AuthenticationManager : MonoBehaviour
{

    //Gameobjects of the Text Fields
    [Header("Gameobjects Text Fields")]
    public GameObject FieldUsername;
    public GameObject FieldPassword;

    //Text fields that contain the information
    [Header("InputField Fields")]
    public InputField TextEmail;
    public InputField TextUsername;
    public InputField TextPassword;

    [Header("Login Feedback")]
    //Text field for the feedback of the login/register progress
    public Text LoginFeedback;

    WWWForm form;

    public void Login_Press()
    {
        //Start login in and starting the corountine
        LoginFeedback.text = "Logging in...";
        StartCoroutine("RequestLogin");
    }
    public void Register_Press()
    {
        //Start login in and starting the corountine
        LoginFeedback.text = "Registering...";
        StartCoroutine("RequestRegister");
    }

    //Corountine that goes through the Login process
    public IEnumerator RequestLogin()
    {
        //Assigning strings from the text
        string email = TextUsername.text;
        string password = TextPassword.text;
        LoginFeedback.color = Color.white;

        //Init form and give them the email and password
        form = new WWWForm();
        form.AddField("usernamePost", email);
        form.AddField("passwordPost", password);


        //Login to the website and wait for a response
        WWW w = new WWW("http://81.169.177.181/OLP/action_login.php", form);
        yield return w;

        //Check if the response if empty or not
        if (string.IsNullOrEmpty(w.error))
        {
            //Return the json array and put it in the C# User class
            User user = JsonUtility.FromJson<User>(w.text);
            if (user.success == true)
            {
                //Check if there is any error in the class. If there is return the error
                if (user.error != "")
                {
                    LoginFeedback.text = user.error;
                }
                else
                {
                    Debug.Log("user: " + user.email);
                    //Login the user and redirect it to a new scene
                    LoginFeedback.text = "login successful.";
                    AppManager.instance.SetUser(user);
                    SceneManager.LoadScene("Main", LoadSceneMode.Single);
                }
                //If the JsonArary is empty return this string in the feedback
            }
            else
            {
                LoginFeedback.text = "An error occured";
            }

            //If the string is empty return this string in the feedback
        }
        else
        {
            // error
            LoginFeedback.text = "An error occured.";
        }


    }

    public IEnumerator RequestRegister()
    {
        string email = TextEmail.text;
        string username = TextUsername.text;
        string password = TextPassword.text;
        form = new WWWForm();
        form.AddField("namePost", username);
        form.AddField("usernamePost", username);
        form.AddField("passwordPost", password);
        form.AddField("emailPost", email);

        WWW w = new WWW("http://81.169.177.181/OLP/action_register.php", form);
        yield return w;
        LoginFeedback.color = Color.white;
        Debug.Log(w.text);
        if (string.IsNullOrEmpty(w.error))
        {
            User user = JsonUtility.FromJson<User>(w.text);
            if (user.success == true)
            {
                if (user.error != "")
                {
                    LoginFeedback.text = user.error;
                }
                else
                {
                    AppManager.instance.SetUser(user);
                    SceneManager.LoadScene("Tutorial", LoadSceneMode.Single);
                    LoginFeedback.text = "You can log in now";
                }
            }
            else
            {
                LoginFeedback.text = "An error occured";
            }

            // todo: launch the game (player)
        }
        else
        {
            // error
            LoginFeedback.text = "An error occured.";
        }


    }
}

[System.Serializable]
public class User
{
    public bool success;
    public string error;
    public string email;
    public int ID;
    public int population;
    public int currency;
    public int environment;
    public int happiness;
    public string name;
}