using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DailyReward : MonoBehaviour
{
    public GameObject Displaywindow;
    public Sprite Gold;
    public Sprite Diamond;
    public List<Sprite> Furniture;//needs to be shop variant
    public int[] AmountOfFurniture;//furnitur index
    public FurnitureData data;
    public int AmmountToAwarded = 0;
    public bool canGetReward = false;
    public int upperboundfortier2items =30;
    public TMP_Text timer;
    public TMP_Text txtreward;
    private string datenextreward;
    public GameObject canvastohide;

    // Start is called before the first frame update
    void Start()
    {
        data = (FurnitureData)Resources.Load("GameData");
        if(data.LastRewardDate + 86400 <= System.DateTime.UtcNow.Ticks)
        {
            canGetReward = true;
        }
        if (canvastohide == null)
        {
            canvastohide = GameObject.Find("canvastohide");
        }
        canvastohide.SetActive(false);
        Furniture =data.Furniture;
        AmountOfFurniture = data.AmountOfFurniture;

        DateTime lastRewardDate = new DateTime(data.LastRewardDate, DateTimeKind.Utc);

        // Add 1 day to get the next reward time
        DateTime nextRewardDate = lastRewardDate.AddDays(1);

        // Convert to local time if needed
        DateTime localTime = nextRewardDate.ToLocalTime();

        // Display
        //Debug.Log("Next Reward Date: " + localTime);
        timer.text = localTime.ToString("yyyy-MM-dd HH:mm:ss");
    }
    // Update is called once per frame
    public void GetReward()
    {
        if (canGetReward)
        {

            if (canvastohide == null)
            {
                canvastohide = GameObject.Find("canvastohide");
            }
            canvastohide.SetActive(true);

            //reset
            data.LastRewardDate = System.DateTime.UtcNow.Ticks;//todays  seconds
            
            //genreward
            int temp = UnityEngine.Random.Range(0, 100);
            if (temp >= 97)//7%
            {
                //furniture
                int newtemp = UnityEngine.Random.Range(0, upperboundfortier2items);
                Displaywindow.GetComponent<SpriteRenderer>().sprite = Furniture[newtemp];
                AmountOfFurniture[newtemp]++;
                AmmountToAwarded = 1;
                txtreward.text = AmmountToAwarded.ToString();
                canGetReward=false;

            }
            else if (temp >= 67)//33%
            {
                //diamond
                AmmountToAwarded = UnityEngine.Random.Range(10, 20);
                Displaywindow.GetComponent<SpriteRenderer>().sprite = Diamond;
                data.Diamond = data.Diamond + AmmountToAwarded;
                txtreward.text = AmmountToAwarded.ToString();
                canGetReward = false;   


            }
            else //60%
            {
                //gold
                AmmountToAwarded = UnityEngine.Random.Range(20, 30);
                Displaywindow.GetComponent<SpriteRenderer>().sprite = Diamond;
                data.Gold = data.Gold + AmmountToAwarded;
                txtreward.text = AmmountToAwarded.ToString();
                canGetReward = false;
            }
            timer.text = datenextreward;

        }
 
    }
    public void ClaimReward()
    {
       if (canvastohide == null)
       {
            canvastohide = GameObject.Find("canvastohide");
       }
        canvastohide.SetActive(false);

    }
}

