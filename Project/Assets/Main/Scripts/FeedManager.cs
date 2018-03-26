using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class FeedManager : MonoBehaviour {
    public ItemManager itemmanager;
    public List<GameObject> items;
    private void Start()
    {
        StartCoroutine("RequestUsers");
        EventManager.AddFeed += AddItems;
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
                Debug.Log("user_id " + user.user_id);
                user_id.AddField("user_id", user.user_id);
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
                                UD.text = UI.name + " has bought: " + item.name;
                                EventManager.Create_UserData(UD);
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
        StartCoroutine("LoopThroughList");
    }
    public void AddItems(GameObject go)
    {
        items.Add(go);
    }
    public IEnumerator LoopThroughList()
    {
        while(items.Count > 0)
        {
            foreach(GameObject g in items)
            {
                g.transform.DOLocalMoveX(-21, 15);
                yield return new WaitForSeconds(6);
            }
            foreach (GameObject g in items)
            {
                g.transform.localPosition = new Vector3(21, 0, 0);
            }
        }
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