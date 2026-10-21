using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    public TMP_Text hiScoreText;
    public TMP_Text currentScoreText;

    // Start is called before the first frame update
    void Start()
    {
        int originalHiScore = PlayerPrefs.GetInt("High Score", 0);
        hiScoreText.text = "High Score: " + originalHiScore;

        int currentScore = PlayerPrefs.GetInt("Current Score", 0);
        currentScoreText.text = "Current Score: " + currentScore;
    }
}
