using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreKeeper : MonoBehaviour
{
	// private int score = 0;
	// private int highScore = 0;

	public static int Score { get; private set; }
	public static int HighScore { get; private set; }


	// Start is called before the first frame update
	void Start()
	{
		// Load the saved score
		Score = LoadScore();

		// Load the high score
		HighScore = LoadHighScore();

		// Create a repeating call of the 'IncreaseScore' method. This will then increase the score
		// by one every second. (Completes Week One task 'Add score; 1 point per second')
		// if ( SceneManager.GetActiveScene().name != "End of Play Screen" )
		//, { InvokeRepeating( "IncreaseScore", 0f, 1f ); }

		// Debug.Log( HighScore );
	}

	// Update is called once per frame
	void Update()
	{
		// Debug.Log( $"Current score: {this.score}" );
	}

	private void OnApplicationQuit()
	{
		// Update the saved high score when the game closes
		UpdateHighScore();
	}

	void IncreaseScore() { Score++; }
	void ResetScore() { Score = 0; }
	int LoadScore() { return PlayerPrefs.GetInt( "playerScore" ); }
	void SaveScore() { PlayerPrefs.SetInt( "playerScore", Score ); }

	int LoadHighScore() { return PlayerPrefs.GetInt( "playerHighScore" ); }
	private static void SaveHighScore() { PlayerPrefs.SetInt( "playerHighScore", HighScore ); }

	public static void UpdateHighScore()
	{
		if ( Score > HighScore )
		{
			HighScore = Score;
			SaveHighScore();
		}
	}

	public static void ModifyScore( int amount )
	{
		Score += amount;
	}
}
