using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ScoreTracker : MonoBehaviour
{
    public Text hiScoreText;
    public Text currentScoreText;

    // Start is called before the first frame update
    void Start()
    {
        int originalHiScore = PlayerPrefs.GetInt("High Score", 0);
        hiScoreText.text = "High Score: " + originalHiScore;

        int currentScore = PlayerPrefs.GetInt("Current Score", 0);
        currentScoreText.text = "Current Score: " + currentScore;
    }
}
