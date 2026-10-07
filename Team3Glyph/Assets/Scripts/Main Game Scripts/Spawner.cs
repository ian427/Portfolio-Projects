using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	[SerializeField] private SwipDirectionTracker Track;
	private int SpawnCount = 0;
	private int GlyphsBetweenRare;

	// Start is called before the first frame update
	void Start()
	{
		GlyphsBetweenRare = Random.Range( 14, 18 );
	}

	public void Spawn()
	{
		Track.omniSwipes = 0;
		
		// Instantiate(Track.directionalGlyph[Index], transform.position, transform.rotation);

		if ( SpawnCount == GlyphsBetweenRare )
		{
			SpawnRareGlyph();
			SpawnCount = 0;
		}
		else
		{
			SpawnGlyph();
		}

		SpawnCount++;
	}

	private void SpawnGlyph()
	{
		int Index = Random.Range( 0, Track.directionalGlyph.Length );
		Track.directionalGlyph[ Index ].GetComponent<Glyph>().Activate();
		Track.CurrentGlyph = Index;
	}

	void SpawnRareGlyph()
	{
		GlyphsBetweenRare = Random.Range( 14, 18 );
		int Index = Random.Range( 0, Track.RareGlyphsToCollect.Length );

		while ( !Track.RareGlyphsToCollect[ Index ] )
		{
			Index = Random.Range( 0, Track.RareGlyphsCollected.Length );
		}

		Track.RareGlyphsToCollect[ Index ].GetComponent<Glyph>().Activate();
		Track.CurrentGlyph = 30;
		Track.RareGlyph = Index;
	}
}
