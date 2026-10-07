using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Shrink : MonoBehaviour
{
	[SerializeField] private float shrinkSpeed = 0.3f;

	public GameObject Square;
	public bool squareExists = true;
	public Spawner Spawner;
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		float width = Square.GetComponent<Transform>().localScale.x;
		float height = Square.GetComponent<Transform>().localScale.y;
		if ( width >= 0 && height >= 0 )
		{ 
			Square.transform.localScale -= new Vector3( shrinkSpeed, shrinkSpeed, 0 ) * Time.deltaTime;
		}

		//Destroys glyph when too small
		if ( width <= 0 && height <= 0 )
		{
			// Destroy( Square );
			//These may be used for glyph spawning at a later date
			//Spawner.Spawn();
			//squareExists = false;
		}

		/*if (squareExists == false)
        
        {
            int AncientGlyphChance = Random.Range(1, 11);
            if (AncientGlyphChance != 10)
            { Instantiate(Square, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
                Square.transform.localScale = new Vector3(6.0f, 6.0f, 1.0f);
                squareExists = true;
            }
        }*/


	}
}
