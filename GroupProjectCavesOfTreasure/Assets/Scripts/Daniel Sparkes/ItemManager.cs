using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
//using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using System;
using System.ComponentModel;
using UnityEngine.UIElements;


public class ItemManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI GoldNumber;
    [SerializeField] private TextMeshProUGUI DiamondsNumber;
    [SerializeField] private TextMeshProUGUI MysticGemsNumber;
     public FurnitureData data;
     public int goldAmount;
     public int diamondsAmount;
     public int mysticGemsAmount;
     private int random;
    [SerializeField] private List<Sprite> Furniture;//needs to be shop variant
    [SerializeField] public int[] AmountOfFurniture;
     private int TotalItiems;
    public GameObject []ButtonsT1;
    public GameObject []ButtonsT2;
    public GameObject ButtonT3;
    [SerializeField] private int IndexOfLastTier1;
    [SerializeField] private int IndexOfLastTier2;
    [SerializeField] private int IndexOfLastTier3;
    [SerializeField] private List<Sprite> CurrentlyDisplayedT1;
    [SerializeField] private List<Sprite> CurrentlyDisplayedT2;
    [SerializeField] private Sprite currentlydisplayedT3;
     private TimeSpan currentTimeAsTimeSpan;
    private TimeSpan LastTimeStamp;
    
    private bool Canupdate = false;
    void Awake()
    { 
        Debug.Log("current" + currentTimeAsTimeSpan);
        Debug.Log("last" + LastTimeStamp);
            data = (FurnitureData)Resources.Load("GameData");
        //data.LoadGameData();
            goldAmount = data.Gold;
            diamondsAmount = data.Diamond;
            mysticGemsAmount = data.Gem;
           
            Furniture = data.Furniture;
            AmountOfFurniture = data.AmountOfFurniture;
        LastTimeStamp = data.LastDate;

        data.Gold = goldAmount;
        GoldNumber.text = "Gold: " + goldAmount;
        data.Diamond = diamondsAmount;
        DiamondsNumber.text = "Diamonds: " + diamondsAmount;
        data.Gem = mysticGemsAmount;
        MysticGemsNumber.text = "Gems: " + mysticGemsAmount;

        currentTimeAsTimeSpan = TimeSpan.FromTicks(System.DateTime.UtcNow.Ticks);//time now
        //Debug.Log(currentTimeAsTimeSpan);
        CurrentlyDisplayedT1 = data.CurrentlyDisplayedT1;
        CurrentlyDisplayedT2 = data.CurrentlyDisplayedT2;
        currentlydisplayedT3 = data.currentlydisplayedT3;
        if (currentTimeAsTimeSpan <= LastTimeStamp)
        {
            Canupdate = true;
            data.LastDate = currentTimeAsTimeSpan + TimeSpan.FromDays(1);//when next shop update 
            Debug.Log("UpdatedShop");
        }
        //Tier 1
        for (int i = 0; i < ButtonsT1.Length; i++)
        {
            ButtonsT1[i].GetComponent<BuyItem>().CurrentlyDisplayedItem = CurrentlyDisplayedT1[i];
        }
        //Tier 2
        for (int i = 0; i < ButtonsT2.Length; i++)
        {
            ButtonsT2[i].GetComponent<BuyItem>().CurrentlyDisplayedItem = CurrentlyDisplayedT2[i];
        }
        //Tier 3
        ButtonT3.GetComponent<BuyItem>().CurrentlyDisplayedItem = currentlydisplayedT3;
    

       
            if (Canupdate)
            {

                //Tier 1
                for (int i = 0; i < ButtonsT1.Length; i++)
                {
                    //Generate random number
                    random = UnityEngine.Random.Range(0, IndexOfLastTier1);//index into furniture
                    CurrentlyDisplayedT1[i] = Furniture[random];
                    ButtonsT1[i].GetComponent<BuyItem>().CurrentlyDisplayedItem = CurrentlyDisplayedT1[i];
                }
                //Tier 2
                for (int i = 0; i < ButtonsT2.Length; i++)
                {
                random = UnityEngine.Random.Range(IndexOfLastTier1, IndexOfLastTier2);//index into furniture
                CurrentlyDisplayedT2[i] = Furniture[random];
                ButtonsT2[i].GetComponent<BuyItem>().CurrentlyDisplayedItem = CurrentlyDisplayedT2[i];
                }
                //Tier 3
                random = UnityEngine.Random.Range(IndexOfLastTier1, Furniture.Count);//index into furniture
                currentlydisplayedT3 = Furniture[random];
                ButtonT3.GetComponent<BuyItem>().CurrentlyDisplayedItem = currentlydisplayedT3;
           

            }
            
    }
    public int FindItem(Sprite Item)
    {
        int answer = 0;
        for (int i = 0; i < Furniture.Count; i++)
        {
            if (Furniture[i] == Item)
            {
                answer = i;

            }
        }
        return answer;
    }
    public void AddItem(Sprite Item)
    {
        int temp = FindItem(Item);
        AmountOfFurniture[temp]++;
    }
    public void updatedata()
    {
        data.Gold = goldAmount;
        
        data.Diamond = diamondsAmount;
        
        data.Gem = mysticGemsAmount;
       

        data.Furniture = Furniture;
        data.AmountOfFurniture = AmountOfFurniture;
        data.LastDate = LastTimeStamp;
        
        data.CurrentlyDisplayedT1 = CurrentlyDisplayedT1;
        data.CurrentlyDisplayedT2 = CurrentlyDisplayedT2;
        data.currentlydisplayedT3 = currentlydisplayedT3;

       // data.SaveGameData();
    }
    void Update()
    {
        GoldNumber.text = "Gold: " + goldAmount;
        DiamondsNumber.text = "Diamonds: " + diamondsAmount;
        MysticGemsNumber.text = "Gems: " + mysticGemsAmount;
        if (Input.GetKeyDown(KeyCode.M))
        {
            goldAmount += 500;
            diamondsAmount += 100;
            mysticGemsAmount += 10;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            //Tier 1
            for (int i = 0; i < ButtonsT1.Length; i++)
            {
                //Generate random number
                random = UnityEngine.Random.Range(0, IndexOfLastTier1);//index into furniture
                CurrentlyDisplayedT1[i] = Furniture[random];
                ButtonsT1[i].GetComponent<BuyItem>().CurrentlyDisplayedItem = CurrentlyDisplayedT1[i];
                ButtonsT1[i].GetComponent<BuyItem>().Updatedisplay();
            }
            //Tier 2
            for (int i = 0; i < ButtonsT2.Length; i++)
            {
                random = UnityEngine.Random.Range(IndexOfLastTier1, IndexOfLastTier2);//index into furniture
                CurrentlyDisplayedT2[i] = Furniture[random];
                ButtonsT2[i].GetComponent<BuyItem>().CurrentlyDisplayedItem = CurrentlyDisplayedT2[i];
                ButtonsT2[i].GetComponent<BuyItem>().Updatedisplay();
            }
            //Tier 3
            random = UnityEngine.Random.Range(IndexOfLastTier2, Furniture.Count);//index into furniture
            currentlydisplayedT3 = Furniture[random];
            ButtonT3.GetComponent<BuyItem>().CurrentlyDisplayedItem = currentlydisplayedT3;
            ButtonT3.GetComponent<BuyItem>().Updatedisplay();


        }
    }
    
}