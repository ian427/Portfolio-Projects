using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glyph : MonoBehaviour
{
    // Set the default shrink speed
    [SerializeField] private float shrinkSpeed = 0.5f; //Bigger number means faster shrink speed

    // Shrinking?
    [SerializeField] private bool shouldShrink = false;

    // Update is called once per frame
    void Update()
    {
        if ( shouldShrink )
        {
            Shrink();
        }
    }

    public void Deactivate()
    {
        shouldShrink = false;
        Reset();
		gameObject.SetActive( false );
	}

    public void Activate()
    {
        shouldShrink = true;
        gameObject.SetActive( true );
    }

    private void Shrink()
    {
        float width = gameObject.transform.localScale.x; 
        float height = gameObject.transform.localScale.y; 

        if ( width >= 0 && height >= 0 )
        {
            gameObject.transform.localScale -= new Vector3( shrinkSpeed, shrinkSpeed, 0 ) * Time.deltaTime;
        }
        else
        {
            Deactivate();
            gameObject.GetComponentInParent<Spawner>().Spawn();
        }
    }

    private void Reset()
    {
        gameObject.transform.localScale = Vector3.one * 1.1f;
    }

    public float GetCurrentSize()
    {
        float size = transform.localScale.x;
        return size;
    }
}
