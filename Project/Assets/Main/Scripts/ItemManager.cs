using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemManager : MonoBehaviour {
    public Item tempitem;
    [SerializeField]
    List<Item> _owneditems;
    [SerializeField]
    public List<Item> _items;

    [SerializeField]
    List<Item_ID> _itemid;

    private void Start()
    {
        EventManager.BuyStoreItem += BuyItem;
        Request_Items();
    }
    public void Request_Items()
    {
        foreach (Transform child in GetComponent<ShopManager>().ItemHolder.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        StartCoroutine("RequestItems");
    }
    public IEnumerator RequestItems()
    {
        _items = new List<Item>();
        _items.Clear();
        //ClearModifiers();
        _itemid.Clear();
        WWW itemdata = new WWW("http://81.169.177.181/olp/item_list.php");
        yield return itemdata;
        if (string.IsNullOrEmpty(itemdata.error))
        {
            _items = new List<Item>();
            _items = JsonHelper.getJsonArray<Item>(itemdata.text).ToList<Item>();
            WWWForm user_id = new WWWForm();
            user_id.AddField("user_id", AppManager.instance.User.ID);
            WWW owneditemsdata = new WWW("http://81.169.177.181/olp/owned_items.php", user_id);
            yield return owneditemsdata;

            if (string.IsNullOrEmpty(owneditemsdata.error))
            {
                _itemid = new List<Item_ID>();
                _itemid = JsonHelper.getJsonArray<Item_ID>(owneditemsdata.text).ToList<Item_ID>();
                for (int i = 0; i < _itemid.Count; i++)
                {
                    foreach(Item item in _items)
                    {
                        if(item.ID == _itemid[i].item)
                        {
                            _owneditems.Add(item);
                        }
                    }

                }
            }
            else
            {
                Debug.Log("No items");
            }
        }
        else
        {
            Debug.LogError("ERROR FATAL");
        }
        StoreInit();
    }

    public void StoreInit()
    {
        foreach(Item item in _items)
        {
            bool test = _owneditems.Any(x => x.ID == item.ID);
            if(test)
            {
                item.owned = true;
            }
            EventManager.Create_StoreItem(item);
        }
    }

    public void BuyItem(Item _item)
    {
        tempitem = _item;
        StartCoroutine("Buy_Item");
    }
    public IEnumerator Buy_Item()
    {
        WWWForm user_id = new WWWForm();
        user_id.AddField("user_id", AppManager.instance.User.ID);
        user_id.AddField("item_id", tempitem.ID);
        WWW itemdata = new WWW("http://81.169.177.181/olp/buy_item.php", user_id);
        yield return itemdata;
        if (string.IsNullOrEmpty(itemdata.error))
        {
            SuccessCheck SC = JsonUtility.FromJson<SuccessCheck>(itemdata.text);
            if (SC.success)
            {
                ResourceMessage rm = new ResourceMessage();
                //rm.Initialise(Resources.currency, -tempitem.costs, 0, 0);
                
                EventManager._SendResourceMessage(rm);
                AppManager.instance.StartCoroutine("UpdateResources");
                foreach (Transform child in GetComponent<ShopManager>().ItemHolder.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }
                StartCoroutine("RequestItems");
                EventManager.Parse_MapItem(tempitem);
                //AppManager.instance.ParseTowardsResources();
            }
        }
        else
        {
            Debug.LogError("ERROR FATAL");
        }
    }
}

[System.Serializable]
public class Item
{
    public int ID;
    public string name;
    public int sprite;
    public string description;
    public int costs;
    public int days;
    public float air_pollution;
    public float water_pollution;
    public float soil_pollution;
    public float biodiversity;
    public float population;
    public float currency;
    public float landuse;
    public bool owned = false;
    public Sprite _sprite;
}

[System.Serializable]
public class Item_ID
{
    public int item;
}

public class SuccessCheck
{
    public bool success;
}