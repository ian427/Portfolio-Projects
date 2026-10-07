using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RareGlyphTracker : MonoBehaviour
{
	// Create a dictionary that stores the states of the Rare Glyphs. By default, the dictionary is empty, as the
	// GenerateRareGlyphsDict method generates the key/value pairs. This can obviously be overidden with data that
	// has been saved.
	private static Dictionary<string, bool> rareGlyphs;

	private static bool rareGlyphsSaved;

	private void Awake()
	{
		rareGlyphsSaved = PlayerPrefs.HasKey( "rareGlyphsSaved" );
		Debug.Log( $"rareGlyphsSaved {rareGlyphsSaved}" );

		// Generate the Glyphs dictionary
		rareGlyphs = GenerateRareGlyphsDict();
	}

	private void Start()
	{
		
	}

	// Iterates through 0 to 25 and generates the name ('RareGlyph0', 'RareGlyph25', etc) as well as setting the 
	// default state to false until saved data can override it. 
	private static Dictionary<string, bool> GenerateRareGlyphsDict()
	{
		Dictionary<string, bool> temp = new();

		Debug.Log( "Loading rare glyphs..." );
		for ( int x = 0; x <= 25; x++ )
		{
			if ( rareGlyphsSaved )
			{
				bool found = PlayerPrefs.GetInt( $"rareGlyphsFound_RareGlyph{x}" ) == 1;
				temp.Add( $"RareGlyph{x}", found );
				Debug.Log( $"RareGlyph{x} found, state: {found}" );
			}
			else
			{
				temp.Add( $"RareGlyph{x}", false );
				//temp.Add( $"RareGlyph{x}", Random.value > 0.5 ); // This is just for debug purposes
				Debug.Log( $"Didn't find a saved state for RareGlyph{x}" );
			}
		}

		return temp;
	}

	public static bool GetRareGlyphFound( int rareGlyph )
	{
		return rareGlyphs[ $"RareGlyph{rareGlyph}" ];
	}

	public static void SetRareGlyphFound( int rareGlyph, bool found )
	{
		rareGlyphs[ $"RareGlyph{rareGlyph}" ] = found;
	}

	public static void SaveRareGlyphs()
	{
		if ( !PlayerPrefs.HasKey( "rareGlyphsSaved" ) ) PlayerPrefs.SetInt( "rareGlyphsSaved", 1 );

		for ( int x = 0; x < 25; x++ )
		{
			// Convert bool to an int for saving
			int found = rareGlyphs[ $"RareGlyph{x}" ] ? 1 : 0;
			PlayerPrefs.SetInt( $"rareGlyphsFound_RareGlyph{x}", found );
		}
	}
}
