using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FacebookManager : MonoBehaviour
{
    public Text LoginFeedback;
    private AccessToken token;
    public string Name;
    public string Email;

    WWWForm form;
    private void Awake()
    {
        if (!FB.IsInitialized)
        {
            FB.Init();
        }
        else
        {
            FB.ActivateApp();
        }
    }
    public void Register()
    {
        List<string> permissions = new List<string>();
        permissions.Add("public_profile");
        FB.LogInWithReadPermissions(permissions, OnFBRegister);
    }
    public void LogIn()
    {
        List<string> permissions = new List<string>();
        permissions.Add("public_profile");
        FB.LogInWithReadPermissions(permissions, OnFBLogin);
    }

    public void OnFBLogin(IResult result)
    {
        if (FB.IsLoggedIn)
        {
            Debug.Log("test");
            AccessToken token = AccessToken.CurrentAccessToken;
            LoginFeedback.text = token.UserId;
            GetUserInfo();
            StartCoroutine("RequestLoginFB");
        }
        else
        {
            Debug.Log("Canceled Login");
        }
    }

    public void OnFBRegister(IResult result)
    {
        if (FB.IsLoggedIn)
        {
            Debug.Log("test");
            token = AccessToken.CurrentAccessToken;
            LoginFeedback.text = token.UserId;
            GetUserInfo();
            StartCoroutine("RequestRegisterFB");
        }
        else
        {
            Debug.Log("Canceled Login");
        }
    }

    private void GetUserInfo()
    {
        FB.API("/me?fields=first_name,email", HttpMethod.GET, ParseUserValues, new Dictionary<string, string>(){});
    }

    private void ParseUserValues(IResult result)
    {
        Debug.Log("print username");
        if (result.Error == null)
        {
            Email = "noemail@fb.com";
            Name = "" + result.ResultDictionary["first_name"];
        }
    }


    public IEnumerator RequestRegisterFB()
    {
        yield return new WaitForSeconds(1);
        string username = token.UserId;
        string password = "fb";
        form = new WWWForm();
        form.AddField("emailPost", Email);
        form.AddField("usernamePost", username);
        form.AddField("namePost", Name);
        form.AddField("passwordPost", password);

        WWW w = new WWW("http://81.169.177.181/OLP/action_register.php", form);
        yield return w;
        Debug.Log(w.text);
        if (string.IsNullOrEmpty(w.error))
        {
            User user = JsonUtility.FromJson<User>(w.text);
            Debug.Log("username" + user.ID);
            if (user.success == true)
            {
                if (user.error != "")
                {
                    LoginFeedback.text = user.error;
                }
                else
                {
                    LoginFeedback.text = "You can log in now";
                    SceneManager.LoadScene("Tutorial", LoadSceneMode.Single);
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
            //LoginFeedback.text = "An error occured.";
        }
    }
    //Corountine that goes through the Login process
    public IEnumerator RequestLoginFB()
    {
        yield return new WaitForSeconds(1);
        //Assigning strings from the text
        string username = AccessToken.CurrentAccessToken.UserId;

        string password = "fb";
        LoginFeedback.color = Color.white;

        //Init form and give them the email and password
        form = new WWWForm();
        form.AddField("emailPost", Email);
        form.AddField("usernamePost", username);
        form.AddField("namePost", Name);
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
                    //Login the user and redirect it to a new scene
                    LoginFeedback.text = "login successful.";
                    //AppManager.instance.SetUser(user);
                    SceneManager.LoadScene("Main", LoadSceneMode.Single);
                }
                //If the JsonArary is empty return this string in the feedback
            }
            else
            {
                LoginFeedback.text = user.error;
            }

            //If the string is empty return this string in the feedback
        }
        else
        {
            // error
            LoginFeedback.text = "Fatal error";
        }


    }
}
