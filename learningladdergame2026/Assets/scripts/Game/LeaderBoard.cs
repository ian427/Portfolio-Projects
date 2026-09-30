using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class LeaderBoard : MonoBehaviour
{
    private Savingsystem saver;
    public List<Savingsystem.LeaderBoardEntry> Leaderboard = new List<Savingsystem.LeaderBoardEntry>();
    [SerializeField] private TMP_InputField InputFieldName;
    public TMP_Text ScoreEntertxt;


    [System.Serializable]
    
    public class LeaderBoarddisplay
    {
         public TMP_Text Lscore;
        public TMP_Text LName;
        public void Initialize()
        {
            Lscore.text = "Score 0";
            LName.text = "Enter Name";
        }

    }
    public List<LeaderBoarddisplay> display = new List<LeaderBoarddisplay>(10);
    // Start is called before the first frame update
    private void UpdateDisplay(int index)
    {
        display[index].LName.text = Leaderboard[index].name;
        display[index].Lscore.text = Leaderboard[index].score.ToString();
    }
    void Start()
    {
        saver = GameObject.Find("SavingSystem").GetComponent<Savingsystem>();
        Leaderboard = saver.GetLeaderboard();
        for (int i = 0; i < display.Count; i++)
        {
            UpdateDisplay(i);

        }
    }
    public void NewEntry()//call with button
    {
        
        string name = "Enter Name";
        string strscore = "0";
        //valid data input field check
        name = InputFieldName.text;
        strscore = ScoreEntertxt.text;
        bool onlyDigits = strscore.All(char.IsDigit);
        if (name == "Enter Name")
        {
            onlyDigits = false;
        }
        if (!onlyDigits)//handel error
        {
            return;
        }
        //format
        int score = int.Parse(strscore);
        //call insert
        InsertScore(name, score);
    }

    private void InsertScore(string name, int score)
    {
        bool MatchNotFound = true;
        for (int i = 0; i < Leaderboard.Count; i++)
        {
            if ((MatchNotFound)&& (Leaderboard[i].score < score))
            {
                MatchNotFound = false;
                Leaderboard[i].score = score;
                Leaderboard[i].name = name;
                UpdateDisplay(i);
            }
        }

    }

}
