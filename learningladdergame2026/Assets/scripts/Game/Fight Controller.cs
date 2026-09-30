using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class FightController : MonoBehaviour
{
    [SerializeField] private PlayerController Player1;
    [SerializeField] private PlayerController Player2;
    public MinigameControler minigame;
    private int player1whiffscounter = 0;
    private int player2whiffscounter = 0;
    [SerializeField] private int PointPerHit;
    [SerializeField] private TMP_Text Player1Scoretxt;
    [SerializeField] private TMP_Text player2Scoretxt;
    [SerializeField] private TMP_Text Player1Roundseontxt;
    [SerializeField] private TMP_Text Player2Roundseontxt;
    private int player1score = 0;
    private int player2score = 0;
    private int Player1RoundsWon = 0;
    private int Player2RoundsWon = 0;
    [SerializeField] private int PointsPerPunch = 100;
    [SerializeField] private int PointsToKnockDown = 100;
    [SerializeField] private int RoundWinThreshold = 2;
    [SerializeField] private UniversalMenuButton WinMenu;
    [SerializeField] private SpawnText text;
    public LeaderBoard leader;
    [SerializeField]private Timer timer;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OutOfTime()
    {
        if (player1score > player2score)
        {
            RoundOver(Player1);
        }
        else
        {
            RoundOver(Player2);
        }
    }
    private void Updatescore(ref int playerscoretracked, int score, TMP_Text text, string str)
    {
        playerscoretracked += score;
        text.text = str + playerscoretracked;

    }
    public void StartMatch()
    {
        player1whiffscounter = 0;
        player2whiffscounter = 0;
        player1score = 0;
        player2score = 0;
        Player1RoundsWon = 0;
        Player2RoundsWon = 0;
        Player1.enabled = true;
        Player2.enabled = true;
        timer.starttimmer();
    }
    private void Matchover(PlayerController Player)
    {
        Player1.enabled = false;
        Player2.enabled = false;
        if (Player == Player1)
        {
            leader.ScoreEntertxt = Player1Scoretxt;
        }
        else
        {
            leader.ScoreEntertxt = player2Scoretxt;
        }

        WinMenu.MenuSwitch();

    }
    private void ResetPlayer(PlayerController player)
    {
        player.isdown = false;
        player.CantakeAction = true;
        player.CanBeHit = true;
        player.isBlocking = false;
        player.anim.Play(player.IdleAnimation);
        
    }
    public void RoundOver(PlayerController Player)
    {
        player1whiffscounter = 0;
        player2whiffscounter = 0;
        player1score = 0;
        player2score = 0;
        Updatescore(ref player1score, 0, Player1Scoretxt, "Score");
        Updatescore(ref player2score, 0, player2Scoretxt, "Score");
        ResetPlayer(Player1);
        ResetPlayer(Player2);
        Player1.isdown = false;
        Player2.isdown = false;
        if (Player == Player1)
        {
            //Player2RoundsWon++;
            Updatescore(ref Player2RoundsWon, 1, Player2Roundseontxt, "Rounds Won");
            Player2.CanBeHit = true;
        }
        else
        {
            // Player1RoundsWon++;
            Updatescore(ref Player1RoundsWon, 1, Player1Roundseontxt, "Rounds Won");
            Player1.CanBeHit = true;
        }
        if (Player1RoundsWon >= RoundWinThreshold)
        {
            Matchover(Player1);
        }
        else if (Player2RoundsWon >= RoundWinThreshold)
        {
            Matchover(Player2);
        }
      
    }
    public void Getbackup(PlayerController Player)
    {
        Player.GetBackUp();
        Player1.enabled = true;
        Player2.enabled = true;
    }
    public int CheckPlayer1Wiffs()
    {
        return player1whiffscounter;
    }
    public int CheckPlayer2Wiffs()
    {
        return player2whiffscounter;
    }
    public void ActionTaken(PlayerController player)
    {
        if ((player == Player1) && (Player1.CanBeHit))
        {
            if (Player2.isBlocking)
            {
                Player2.Hit();
                Updatescore(ref player1score, (PointsPerPunch / 2), Player1Scoretxt, "score");
                text.SpawnTxt((PointsPerPunch/2).ToString());
            }
            else if (Player2.CanBeHit)
            {
                Player2.Hit();
                Updatescore(ref player1score, PointsPerPunch, Player1Scoretxt, "score");
                text.SpawnTxt((PointsPerPunch).ToString());
            }
            else
            {
                if (player1whiffscounter < 3)
                {
                    player1whiffscounter++;
                }
                else
                {
                    player1whiffscounter = 0;
                    Player1.Dazed();
                }
            }
        }
        else if ((player == Player2) && (Player2.CanBeHit))
        {
            if (Player1.isBlocking)
            {
                Player1.Hit();
                Updatescore(ref player2score, (PointsPerPunch / 2), player2Scoretxt, "score");
                text.SpawnTxt((PointsPerPunch / 2).ToString());
            }
            else if (Player1.CanBeHit)
            {
                Player1.Hit();
                Updatescore(ref player2score, PointsPerPunch, player2Scoretxt, "score");
                text.SpawnTxt((PointsPerPunch).ToString());
            }
            else
            {
                if (player2whiffscounter < 3)
                {
                    player2whiffscounter++;
                }
                else
                {
                    player2whiffscounter = 0;
                    Player2.Dazed();
                }
            }

        }
        if (player1score > PointsToKnockDown && !Player2.isdown && !Player2.justGotUp)
        {
            Player2.KnockedDown();
            Player2.enabled = false;

        }
        if (player2score > PointsToKnockDown && !Player1.isdown && !Player1.justGotUp)
        {
            Player1.KnockedDown();
            Player1.enabled = false;
        }
    }
}
