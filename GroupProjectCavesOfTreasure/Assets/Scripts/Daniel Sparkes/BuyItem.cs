using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using TMPro;

public class BuyItem : MonoBehaviour
{
    
    public int buttonIndex;
    public TextMeshProUGUI Label;
    int goldPrice = 0;
    int diamondsPrice = 0;
    int mysticGemsPrice = 0;
    int itemID;
    public Sprite CurrentlyDisplayedItem;
    public GameObject Displaywindow;
    private ItemManager manager;
    private SpriteRenderer rend;

    void Start()
    {

        manager = GameObject.Find("Canvas").GetComponent<ItemManager>();
        Displaywindow.GetComponent<SpriteRenderer>().sprite = CurrentlyDisplayedItem;
        if (buttonIndex == 1)
        {
            goldPrice = 200;
            diamondsPrice = 150;
        }
        else if (buttonIndex == 2)
        {
            goldPrice = 600;
            diamondsPrice = 450;
        }
        else
        {
            mysticGemsPrice = 2;
            diamondsPrice = 600;
        }
       
    }

    public void Buy()
    {
        if (GameObject.Find("Canvas").GetComponent<ItemManager>().goldAmount >= goldPrice && GameObject.Find("Canvas").GetComponent<ItemManager>().diamondsAmount >= diamondsPrice && GameObject.Find("Canvas").GetComponent<ItemManager>().mysticGemsAmount >= mysticGemsPrice)
        {
            GameObject.Find("Canvas").GetComponent<ItemManager>().goldAmount -= goldPrice;
            GameObject.Find("Canvas").GetComponent<ItemManager>().diamondsAmount -= diamondsPrice;
            GameObject.Find("Canvas").GetComponent<ItemManager>().diamondsAmount -= mysticGemsPrice;
            itemID = manager.FindItem(CurrentlyDisplayedItem);
            GameObject.Find("Canvas").GetComponent<ItemManager>().AmountOfFurniture[itemID] += 1;
        }
        Debug.Log("itemBrought");
    }
    public void Updatedisplay()
    {
        Displaywindow.GetComponent<SpriteRenderer>().sprite = CurrentlyDisplayedItem;
    }

}
