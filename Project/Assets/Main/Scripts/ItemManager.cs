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
        _owneditems.Clear();
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
                            Debug.Log("item");
                            EventManager.Parse_MapItem(item);
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

                //LIVING RESOURCES
                LivingResource lv = new LivingResource(Resources.airPollution, (int)tempitem.air_pollution, 16, 4, tempitem.message);
                LivingResource lv2 = new LivingResource(Resources.soilPollution, (int)tempitem.soil_pollution, 2, 4, tempitem.message);
                LivingResource lv3= new LivingResource(Resources.waterPollution, (int)tempitem.water_pollution, 2, 4, tempitem.message);
                LivingResource lv4= new LivingResource(Resources.landUse, (int)tempitem.landuse, 2, 4, tempitem.message);
                LivingResource lv5= new LivingResource(Resources.biodiversity, (int)tempitem.biodiversity, 2, 4, tempitem.message);
                LivingResource lv6 = new LivingResource(Resources.currency, (int)tempitem.currency, 2, 4, tempitem.message);
                EventManager._AddLivingResource(lv);
                EventManager._AddLivingResource(lv2);
                EventManager._AddLivingResource(lv3);
                EventManager._AddLivingResource(lv4);
                EventManager._AddLivingResource(lv5);
                EventManager._AddLivingResource(lv6);

                /*rm.Initialise(Resources.airPollution, (int)tempitem.air_pollution);
                rm.Initialise(Resources.soilPollution, (int)tempitem.soil_pollution);
                rm.Initialise(Resources.waterPollution, (int)tempitem.water_pollution);
                rm.Initialise(Resources.landUse, (int)tempitem.landuse);
                rm.Initialise(Resources.biodiversity, (int)tempitem.biodiversity);*/
                AppManager.instance.User.currency -= tempitem.costs;
                EventManager._SendResourceMessage(rm);
                foreach (Transform child in GetComponent<ShopManager>().ItemHolder.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }
                StartCoroutine("RequestItems");
                EventManager.Parse_MapItem(tempitem);
                Debug.Log(tempitem.ID + " bought");
                AppManager.instance.ParseTowardsResources();
                AppManager.instance.StartCoroutine("UpdateResources");
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
    public string message;
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