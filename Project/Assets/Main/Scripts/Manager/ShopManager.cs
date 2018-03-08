using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour {
    public GameObject ItemHolder;
    public GameObject ShopItem;
    public Sprite[] shopsprites;
    void Start()
    {
        EventManager.CreateStoreItem += InitItem;
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
}
