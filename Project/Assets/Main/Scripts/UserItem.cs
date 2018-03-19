using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserItem : MonoBehaviour {
    public Image Sprite;
    public Text text;


    public void SetUser(UserData userdata)
    {
        text.text = userdata.text;
    }
}
