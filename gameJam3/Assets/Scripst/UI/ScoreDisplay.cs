using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    public int score;//score
    public int hiScore;
    public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {

    }  

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + score;

        if (score > hiScore)
        {
            hiScore = score;
        }

        int originalHiScore = PlayerPrefs.GetInt("High Score", 0);
        if (hiScore > originalHiScore)
        {
            PlayerPrefs.SetInt("High Score", hiScore);
        }

        PlayerPrefs.SetInt("Current Score", score);
    }
}
