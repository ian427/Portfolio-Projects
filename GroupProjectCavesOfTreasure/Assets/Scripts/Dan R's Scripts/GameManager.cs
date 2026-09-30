using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private FurnitureData furnitureData;

    public int totalGold;
    public int totalDiamonds;
    public int totalGems;


    void Start()
    {
        furnitureData = (FurnitureData)Resources.Load("GameData");//include anywhere you use furniture date
        //furnitureData.LoadGameData();

        totalGold = furnitureData.Gold;
        totalDiamonds = furnitureData.Diamond;
        totalGems = furnitureData.Gem;
    }

    void Update()
    {
        
    }
}
