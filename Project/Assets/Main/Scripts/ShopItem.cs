using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour {
    [Header("UI:")]
    public Text Name;
    public Text Description;
    public Text Cost;
    public Image Sprite;
    public Image Pollution;
    public Image LandUse;
    public Image BioDiversity;
    public Image Population;
    public Image Currency;
    public Item Item;
    public GameObject Bought;
    public GameObject BuyButton;

    private string path = "Main/Art/UI/ShopIcons/";
    public void SetItem(Item item)
    {
        Item = item;
    }
    public void UpdateStatus()
    {

        Name.text = Item.name;
        Description.text = Item.description;
        Cost.text = Item.costs + " paluta";
        if (Item.owned)
        {
            Bought.SetActive(true);
            BuyButton.SetActive(false);
        }
        CheckResources();
    }
    private void CheckResources()
    {
        if (Item.biodiversity != 0)
            BioDiversity.gameObject.SetActive(true);
        if (Item.land_pollution != 0 || Item.soil_pollution != 0 || Item.water_pollution != 0)
            Pollution.gameObject.SetActive(true);
        if (Item.landuse != 0)
            LandUse.gameObject.SetActive(true);
        if (Item.currency != 0)
            Population.gameObject.SetActive(true);
        if (Item.population != 0)
            Currency.gameObject.SetActive(true);
    }
    public void Buy()
    {
        EventManager.Buy_StoreItem(Item);
    }

}
