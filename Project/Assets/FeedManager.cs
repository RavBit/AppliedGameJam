using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FeedManager : MonoBehaviour {
    public ItemManager itemmanager;
    private void Start()
    {
        StartCoroutine("RequestUsers");
    }
    [SerializeField]
    List<TempUser> _users;

    public IEnumerator RequestUsers()
    {
        _users = new List<TempUser>();
        WWW itemdata = new WWW("http://81.169.177.181/olp/get_useritems.php");
        yield return itemdata;
        if (string.IsNullOrEmpty(itemdata.error))
        {
            _users = new List<TempUser>();
            _users = JsonHelper.getJsonArray<TempUser>(itemdata.text).ToList<TempUser>();

            foreach (TempUser user in _users)
            {
                WWWForm user_id = new WWWForm();
                user_id.AddField("user_id", user.ID);
                WWW _usernamedata = new WWW("http://81.169.177.181/olp/get_user.php", user_id);
                yield return _usernamedata;
                if (string.IsNullOrEmpty(_usernamedata.error))
                {
                    UserID UI = JsonUtility.FromJson<UserID>(_usernamedata.text);
                    if (UI.success)
                    {
                        UserData UD = new UserData();
                        foreach(Item item in itemmanager._items)
                        {
                            if(item.ID == user.item)
                            {
                                UD.text = UI.name + " has build an " + item.name;
                                Debug.Log(UI.name + " has build an " + item.name);
                            }
                        }
                    }

                }
                else
                {
                    Debug.Log("No items");
                }
            }
        }
        else
        {
            Debug.LogError("ERROR FATAL");
        }
        //StoreInit();
    }
}
[System.Serializable]
public class TempUser
{
    public int ID;
    public int user_id;
    public int item;
}
public class UserID
{
    public bool success;
    public string name;
}