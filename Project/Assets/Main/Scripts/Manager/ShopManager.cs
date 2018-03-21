using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour {
    public GameObject ItemHolder;
    public GameObject UserHolder;
    public GameObject UserItem;
    public GameObject ShopItem;
    public Sprite[] shopsprites;
    public Transform SpawnItemLocation;
    void Start()
    {
        EventManager.CreateStoreItem += InitItem;
        EventManager.CreateUserData += InitUser;
        //TODO SET EVENT FOR CREATE USERITEM
    }

    void InitItem(Item _item)
    {
        GameObject go = Instantiate(ShopItem, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        go.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        go.transform.parent = ItemHolder.transform;
        go.transform.localPosition = Vector3.zero;
        go.GetComponent<ShopItem>().Sprite.sprite = shopsprites[_item.sprite];
        go.GetComponent<ShopItem>().SetItem(_item);
        go.GetComponent<ShopItem>().UpdateStatus();
    }

    void InitUser(UserData _data)
    {
        GameObject go = Instantiate(UserItem, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        go.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        go.transform.parent = UserHolder.transform;
        go.transform.position = SpawnItemLocation.position;
        //go.GetComponent<UserItem>().Sprite.sprite = shopsprites[_data._sprite];
        go.GetComponent<UserItem>().SetUser(_data);
        EventManager.Add_Feed(go);
    }
}
