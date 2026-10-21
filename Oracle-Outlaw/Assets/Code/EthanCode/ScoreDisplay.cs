using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    public TMP_Text scoreText;

    public int score;
    public int hiScore;
    private int pointGain = 1;

    private float timer;
    private float timerThreshold = 1;
    public int timePassedSeconds = 0;
    private int thirtySec = 0;

    void Start()
    {
        scoreText.text = "Score: ";
    }

    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= timerThreshold)
        {
            score += pointGain;
            timer = 0;
            timePassedSeconds++;
            thirtySec++;
            if(thirtySec > 29)
            {
                thirtySec = 0;
                pointGain++;
            }
        }

        scoreText.text = "Score: " + score;

        if (score > hiScore)
        {
            hiScore = score;
        }

        int originalHiScore = PlayerPrefs.GetInt("High Score", 0);
        if(hiScore > originalHiScore)
        {
            PlayerPrefs.SetInt("High Score", hiScore);
        }

        PlayerPrefs.SetInt("Current Score", score);
    }
}
