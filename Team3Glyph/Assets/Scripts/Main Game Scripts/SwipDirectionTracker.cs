using System;
using System.Collections;
using System.Collections.Generic;
// using UnityEditor.SearchService;
using UnityEngine;

public class SwipDirectionTracker : MonoBehaviour
{
	[Header( "Object References" )]
	[SerializeField] private SurvivalMeter survivalMeter;
	[SerializeField] private Spawner spawner;

	[Header( "Audio Clips" )]
	[SerializeField] private AudioClip swipeSuccessAudio;
	[SerializeField] private AudioClip swipeFailAudio;
	[SerializeField] private AudioClip swipeMissedAudio;
	[SerializeField] private AudioClip rareGlyphCaughtAudio;

	private bool RightSwipe;
	private bool LeftSwipe;
	private bool UpSwipe;
	private bool DownSwipe;

	[Header( "Pad States" )]
	public bool RightPadIn = false;
	public bool LeftPadIn = false;
	public bool TopPadIn = false;
	public bool BottomPadIn = false;

	public bool PadOut;

	[Header( "Lists" )]
	//lists use 
	public GameObject[] RareGlyphsToCollect;
	public bool[] RareGlyphsCollected;//turn in to bools
	public GameObject[] directionalGlyph;//spawn list
	public int CurrentGlyph;//index
	public int RareGlyph;//indexrare

	private RareGlyphTracker rareGlyphTracker;

	private int combo = 0;
	public int omniSwipes = 0;

	// Start is called before the first frame update
	void Start()
	{
		// ScoreKeeper = GetComponent<ScoreKeeper>();
		rareGlyphTracker = GetComponent<RareGlyphTracker>();
		Debug.Log( "Start" );
		spawner.Spawn();
		BottomPadIn = false;
	}

	// Update is called once per frame
	void Update()
	{
		if ( RightPadIn || LeftPadIn || TopPadIn || BottomPadIn == true ) //sees if valid swip is in  coming
		{
			Check();
		}
	}

	void Check() // checks the direction of the swipe
	{
		Debug.Log( "CHECK" );
		// if ((PadOut && LeftPadIn == true)&& (PadOut && RightPadIn == true)&& (PadOut && TopPadIn == true) && (PadOut && TopPadIn == true))//swipe omi

		if ( PadOut && LeftPadIn == true )//left swipe
		{
			LeftSwipe = true;
			PadOut = false;
			LeftPadIn = false;
			omniSwipes++;
			GlyphCheck();
			Debug.Log( "swipeLeft" );
		}

		else if ( PadOut && RightPadIn == true )//rightswipe
		{
			RightSwipe = true;
			PadOut = false;
			RightPadIn = false;
			omniSwipes++;
			GlyphCheck();
			Debug.Log( "swipeRight" );
		}

		else if ( PadOut && TopPadIn == true )//swipe up
		{
			UpSwipe = true;
			PadOut = false;
			TopPadIn = false;
			omniSwipes++;
			GlyphCheck();
			Debug.Log( "swipUPe" );
		}

		else if ( PadOut && BottomPadIn == true )//swipe down
		{
			DownSwipe = true;
			PadOut = false;
			BottomPadIn = false;
			omniSwipes++;
			GlyphCheck();
			Debug.Log( "swipeDown" );
		}

		//implement matching Logic

		// As none of the above are true, it's a failed swipe
		combo = 0;
	}
	void GlyphCheck()
	{
		if ( ( CurrentGlyph == 0 ) && ( LeftSwipe == true ) )
		{
			ResetScene();
			LeftSwipe = false;
			Debug.Log( "left" );
		}
		else if ( ( CurrentGlyph == 1 ) && ( RightSwipe == true ) )
		{
			ResetScene();
			RightSwipe = false;
			Debug.Log( "right" );
		}
		else if ( ( CurrentGlyph == 2 ) && ( UpSwipe == true ) )
		{
			ResetScene();
			UpSwipe = false;
			Debug.Log( "up" );
		}
		else if ( ( CurrentGlyph == 3 ) && ( DownSwipe == true ) )
		{
			ResetScene();
			DownSwipe = false;
			Debug.Log( "down" );
		}
		else if ( ( CurrentGlyph == 4 ) && ( omniSwipes == 4 ) )
		{
			ResetScene();
			UpSwipe = false;
			DownSwipe = false;
			RightSwipe = false;
			LeftSwipe = false;

			Debug.Log( "omi" );
		}
		else if ( ( CurrentGlyph == 30 ) && ( UpSwipe || DownSwipe || LeftSwipe || RightSwipe ) )
		{
			RareGlyphCheck();
		}
	}

	void ResetScene( bool wasRareGlyph = false )
	{
		AudioManager.instance.PlayAudio( swipeSuccessAudio, AudioTypes.Effects );
		SurvivalMeter.ResetMeter();
		ScoreKeeper.ModifyScore( 1 );
		combo++;
		omniSwipes = 0;

		if ( !wasRareGlyph )
		{
			directionalGlyph[ CurrentGlyph ].GetComponent<Glyph>().Deactivate();
		}
		else
		{
			RareGlyphsToCollect[ RareGlyph ].GetComponent<Glyph>().Deactivate();
		}

		spawner.Spawn();
		Debug.Log( "Reset" );
	}

	void RareGlyphCheck()
	{
		Debug.Log( "Rare Glyph" );
		// if ( ( RightSwipe | LeftSwipe | UpSwipe | DownSwipe == true ) )
		// {
			for ( int i = 0; i < RareGlyphsToCollect.Length; i++ )
			{
				if ( RareGlyph == i )
				{
					AudioManager.instance.PlayAudio( rareGlyphCaughtAudio, AudioTypes.Effects );
					RareGlyphsCollected[ i ] = true;

					// Update the rare glyph tracker
					RareGlyphTracker.SetRareGlyphFound( i, true );
				}
			}
		// }

		ResetScene( true );
	}
}
