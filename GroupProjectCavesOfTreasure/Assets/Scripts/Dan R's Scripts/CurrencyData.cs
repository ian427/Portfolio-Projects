using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyData : MonoBehaviour
{
    [SerializeField] private FurnitureData furnitureData;

    private int goldTotal;
    private int diamondsTotal;
    private int gemsTotal;

    public TMP_Text goldText;
    public TMP_Text diamondsText;
    public TMP_Text gemsText;

    public TMP_Text[] furnitureText;
    private int[] furnitureValues;
    
    void Start()
    {
        furnitureData = (FurnitureData)Resources.Load("GameData");//include anywhere you use furniture date
        //furnitureData.LoadGameData();

        goldTotal = furnitureData.Gold;
        goldText.text = "Gold: " + goldTotal;

        diamondsTotal = furnitureData.Diamond;
        diamondsText.text = "Diamonds: " + diamondsTotal;

        gemsTotal = furnitureData.Gem;
        gemsText.text = "Gems: " + gemsTotal;

        

    }

    private void Update()
    {
        furnitureText[0].text = "" + furnitureData.AmountOfFurniture[0];
        furnitureText[1].text = "" + furnitureData.AmountOfFurniture[1];
        furnitureText[2].text = "" + furnitureData.AmountOfFurniture[2];
        furnitureText[3].text = "" + furnitureData.AmountOfFurniture[3];
        furnitureText[4].text = "" + furnitureData.AmountOfFurniture[4];
        furnitureText[5].text = "" + furnitureData.AmountOfFurniture[17];
        furnitureText[6].text = "" + furnitureData.AmountOfFurniture[22];
        furnitureText[7].text = "" + furnitureData.AmountOfFurniture[45];
        furnitureText[8].text = "" + furnitureData.AmountOfFurniture[6];
        furnitureText[9].text = "" + furnitureData.AmountOfFurniture[8];
        furnitureText[10].text = "" + furnitureData.AmountOfFurniture[5];
        furnitureText[11].text = "" + furnitureData.AmountOfFurniture[7];
        furnitureText[12].text = "" + furnitureData.AmountOfFurniture[9];
        furnitureText[13].text = "" + furnitureData.AmountOfFurniture[10];
        furnitureText[14].text = "" + furnitureData.AmountOfFurniture[11];
        furnitureText[15].text = "" + furnitureData.AmountOfFurniture[12];
        furnitureText[16].text = "" + furnitureData.AmountOfFurniture[13];
        furnitureText[17].text = "" + furnitureData.AmountOfFurniture[14];
        furnitureText[18].text = "" + furnitureData.AmountOfFurniture[15];
        furnitureText[19].text = "" + furnitureData.AmountOfFurniture[16];
        furnitureText[20].text = "" + furnitureData.AmountOfFurniture[18];
        furnitureText[21].text = "" + furnitureData.AmountOfFurniture[19];
        furnitureText[22].text = "" + furnitureData.AmountOfFurniture[20];
        furnitureText[23].text = "" + furnitureData.AmountOfFurniture[21];
        furnitureText[24].text = "" + furnitureData.AmountOfFurniture[23];
        furnitureText[25].text = "" + furnitureData.AmountOfFurniture[24];
        furnitureText[26].text = "" + furnitureData.AmountOfFurniture[25];
        furnitureText[27].text = "" + furnitureData.AmountOfFurniture[26];
        furnitureText[28].text = "" + furnitureData.AmountOfFurniture[27];
        furnitureText[29].text = "" + furnitureData.AmountOfFurniture[28];
        furnitureText[30].text = "" + furnitureData.AmountOfFurniture[29];
        furnitureText[31].text = "" + furnitureData.AmountOfFurniture[30];
        furnitureText[32].text = "" + furnitureData.AmountOfFurniture[32];
        furnitureText[33].text = "" + furnitureData.AmountOfFurniture[45];
        furnitureText[34].text = "" + furnitureData.AmountOfFurniture[33];
        furnitureText[35].text = "" + furnitureData.AmountOfFurniture[34];
        furnitureText[36].text = "" + furnitureData.AmountOfFurniture[35];
        furnitureText[37].text = "" + furnitureData.AmountOfFurniture[36];
        furnitureText[38].text = "" + furnitureData.AmountOfFurniture[37];
        furnitureText[39].text = "" + furnitureData.AmountOfFurniture[38];
        furnitureText[40].text = "" + furnitureData.AmountOfFurniture[39];
        furnitureText[41].text = "" + furnitureData.AmountOfFurniture[40];
        furnitureText[42].text = "" + furnitureData.AmountOfFurniture[41];
        furnitureText[43].text = "" + furnitureData.AmountOfFurniture[42];
        furnitureText[44].text = "" + furnitureData.AmountOfFurniture[43];
        furnitureText[45].text = "" + furnitureData.AmountOfFurniture[44];
        furnitureText[46].text = "" + furnitureData.AmountOfFurniture[46];
        furnitureText[47].text = "" + furnitureData.AmountOfFurniture[47];
        furnitureText[48].text = "" + furnitureData.AmountOfFurniture[48];
        furnitureText[49].text = "" + furnitureData.AmountOfFurniture[49];
        furnitureText[50].text = "" + furnitureData.AmountOfFurniture[50];
        furnitureText[51].text = "" + furnitureData.AmountOfFurniture[51];
        furnitureText[52].text = "" + furnitureData.AmountOfFurniture[52];
        furnitureText[53].text = "" + furnitureData.AmountOfFurniture[53];
    }
}
