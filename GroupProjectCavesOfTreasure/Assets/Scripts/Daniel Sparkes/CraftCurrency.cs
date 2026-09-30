using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftCurrency : MonoBehaviour
{
    int goldAmount;
    int diamondsAmount;
    int mysticGemsAmount;

    void Start()
    {
        goldAmount = GameObject.Find("Canvas").GetComponent<ItemManager>().goldAmount;
        diamondsAmount = GameObject.Find("Canvas").GetComponent<ItemManager>().diamondsAmount;
        mysticGemsAmount = GameObject.Find("Canvas").GetComponent<ItemManager>().mysticGemsAmount;
    }

    public void Craft()
    {
        if (goldAmount >= 1600 && diamondsAmount >= 850)
        {
            goldAmount -= 1600;
            diamondsAmount -= 850;
            mysticGemsAmount += 1;
            GameObject.Find("Canvas").GetComponent<ItemManager>().goldAmount = goldAmount;
            GameObject.Find("Canvas").GetComponent<ItemManager>().diamondsAmount = diamondsAmount;
            GameObject.Find("Canvas").GetComponent<ItemManager>().mysticGemsAmount = mysticGemsAmount;
        }
    }
}