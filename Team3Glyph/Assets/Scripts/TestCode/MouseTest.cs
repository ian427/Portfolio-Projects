using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTest : MonoBehaviour
{
    /* private float pressedTime = 0f;
    private Vector2 startPos = Vector2.zero;
    private Vector2 endPos = Vector2.zero; */

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    /* void Update()
    {
        if ( Input.GetMouseButtonDown( 0 ) )
        {
            Debug.Log( "Left click pressed." );
            startPos = Input.mousePosition;
            pressedTime = Time.time;
        }
        else if ( Input.GetMouseButton( 0 ) )
        {
            // Debug.Log( $"X: { Input.GetAxis( "Mouse X" ) } \nY: { Input.GetAxis( "Mouse Y" ) }" );
        }
        else if ( Input.GetMouseButtonUp( 0 ) )
        {
            Debug.Log( "Left click released." );
            endPos = Input.mousePosition;

            Debug.Log( $"Start pos: {startPos}\nEnd pos: {endPos}" );

            if ( startPos.x > endPos.x  )
            {
                Debug.Log( "Left swipe" );
            }
            else if ( startPos.x < endPos.x && startPos.x - endPos.y > 50 )
            {
                Debug.Log( "Right swipe" );
            }
             else if ( startPos.y > endPos.y && startPos.y - endPos.y > 50 ) 
            {
                Debug.Log( "Up swipe" );
            }
            else if ( startPos.y < endPos.y && startPos.y - endPos.y > 50 )
            {
                Debug.Log( "Down swipe" );
            }

            pressedTime = 0f; 
        }
    } */
}
