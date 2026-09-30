using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultLoad : MonoBehaviour
{

   
    public List<Sprite> DefaultFurniture;//needs to be shop variant
    public int[] DefaultAmountOfFurniture;//furnitur index
    public int DefaultTotalItiems = 1;

    public int DefaultGold = 0;
    public int DefaultDiamond = 0;
    public int DefaultGem = 0;

  
    public List<Sprite> DefaultCurrentlyDisplayedT1;
    public List<Sprite> DefaultCurrentlyDisplayedT2;
    public Sprite DefaultcurrentlydisplayedT3;

    public int DefaultCurrentNumberOfHunts;
    public List<GameObject> CurrentlyPlacedFurniture;

    FurnitureData data;
    // Start is called before the first frame update
    void Start()
    {
        data = (FurnitureData)Resources.Load("GameData");
        SetDefaults();

    }


    public void OnButtonPress()
    {
        SetDefaults();
        data.SaveGameData();

    }
    public void SetDefaults()
    {
        data = (FurnitureData)Resources.Load("GameData");
        data.Furniture = DefaultFurniture;
        data.AmountOfFurniture = DefaultAmountOfFurniture;
        data.TotalItiems = DefaultTotalItiems;
        data.Gold = DefaultGold;
        data.Diamond = DefaultDiamond;
        data.Gem = DefaultGem;
        data.LastDate = TimeSpan.FromTicks(System.DateTime.UtcNow.Ticks);//time now
        data.Path = Application.dataPath;
        data.CurrentlyDisplayedT1 = DefaultCurrentlyDisplayedT1;
        data.CurrentlyDisplayedT2 = DefaultCurrentlyDisplayedT2;
        data.currentlydisplayedT3 = DefaultcurrentlydisplayedT3;
        data.LastHuntTime = TimeSpan.FromTicks(System.DateTime.UtcNow.Ticks);//time now
        data.LastRewardDate = System.DateTime.UtcNow.Ticks;//time now
        data.CurrentNumberOfHunts = DefaultCurrentNumberOfHunts;
        
    }
}
