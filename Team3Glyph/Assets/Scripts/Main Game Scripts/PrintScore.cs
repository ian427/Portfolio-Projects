using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PrintScore : MonoBehaviour
{
	public TMP_Text textElement;
	[SerializeField] private bool displayScoreText = true;
	[SerializeField] private bool displayHighScore = false; 

	// Start is called before the first frame update
	void Start()
	{
		Debug.Log( ScoreKeeper.Score );
	}

	// Update is called once per frame
	void Update()
	{
		string text; 

		if ( !displayHighScore )
		{
			int score = ScoreKeeper.Score;
			text = displayScoreText ? $"Score: {score}" : $"{score}";
		}
        else
        {
			int highScore = ScoreKeeper.HighScore;
			text = displayScoreText ? $"Score: {highScore}" : $"{highScore}";
		}

		textElement.text = text; 
    }
}
