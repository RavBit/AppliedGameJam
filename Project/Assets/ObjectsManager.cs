﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObjectsManager : MonoBehaviour {
    public GameObject[] Houses;
    public GameObject[] Trees;
    public GameObject[] Item;
    public GameObject Pollution;
    public List<Item> drawnitems;
    // Use this for initialization
    void Start () {
        Invoke("CheckPopulation", 2);
        Invoke("CheckLandUse", 2);
        Invoke("CheckPollution", 2);
        EventManager.ChoiceUnLoad += CheckPopulation;
        EventManager.ChoiceUnLoad += CheckLandUse;
        EventManager.ChoiceUnLoad += CheckPollution;
        EventManager.ParseMapItem += InitItem;
        drawnitems = new List<Item>();
        DisableItems();
    }

    void CheckPollution()
    {
        int pol = AppManager.instance.User.air_pollution;
        Pollution.GetComponent<SpriteRenderer>().DOFade((AppManager.instance.User.air_pollution / 100), 1);
    }
    void DisableItems()
    {
        for (int i = 1; i < Item.Length; i++)
        {
            Item[i].GetComponent<SpriteRenderer>().DOFade(0, 0.1f);
        }
    }
    void CheckPopulation()
    {
        int pop = AppManager.instance.User.population;
        int amount = pop / 1000;
        for (int i = 0; i < Houses.Length; i++)
        {
            if(i > amount || i == 0)
            {
                if (i == 0)
                {

                }
                else
                {
                    Houses[i - 1].SetActive(false);
                    Houses[i - 1].GetComponent<SpriteRenderer>().DOFade(0, 0.5f);
                }
            }
            else
            {
                Houses[i - 1].SetActive(true);
                Houses[i - 1].GetComponent<SpriteRenderer>().DOFade(1, 2f);
            }

        }
    }

    void CheckLandUse()
    {
        int lu = AppManager.instance.User.land_use;
        int amount = lu / 20;
        Debug.Log("Amount: " + amount + " / lu " + lu);
        for (int i = 0; i < Trees.Length; i++)
        {
            if (i > amount || i == 0)
            {
                if (i == 0)
                {

                }
                else
                {
                    Trees[i - 1].SetActive(true);
                    Trees[i - 1].GetComponent<SpriteRenderer>().DOFade(1, 2f);
                }
            }

            else
            {
                Trees[i - 1].SetActive(false);
                Trees[i - 1].GetComponent<SpriteRenderer>().DOFade(0, 0.5f);
            }

        }
    }

    void InitItem(Item item)
    {
        foreach(Item i in drawnitems)
        {
            if(i.ID == item.ID)
            {
                return;
            }
        }
        drawnitems.Add(item);
        Debug.Log("init item " + item.name);
        Item[item.ID].GetComponent<SpriteRenderer>().DOFade(1, 1f);
        EventManager._AddModifierValue(Resources.airPollution, item.air_pollution);
        EventManager._AddModifierValue(Resources.biodiversity, item.biodiversity);
        EventManager._AddModifierValue(Resources.soilPollution, item.soil_pollution);
        EventManager._AddModifierValue(Resources.waterPollution, item.water_pollution);
        EventManager._AddModifierValue(Resources.currency, item.currency);
        EventManager._AddModifierValue(Resources.population, item.population);
        EventManager._AddModifierValue(Resources.landUse, item.landuse);
    }
    // Update is called once per frame
    void Update () {
		
	}
}
