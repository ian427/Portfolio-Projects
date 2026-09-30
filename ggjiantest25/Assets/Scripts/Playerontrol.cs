using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

public class Playerontrol : MonoBehaviour
{
    public int PreviousClick = 0 ;
    public Vector2 ClickPos;
    
    public int x;
    private void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (transform.position.x > -1)
            {
                Vector2 goToNew = new Vector2(transform.position.x - 1, transform.position.y);
                transform.position = goToNew;
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (transform.position.x < 1)
            {
                Vector2 goToNew = new Vector2(transform.position.x + 1, transform.position.y);
                transform.position = goToNew;
            }
        }


        
        //Debug.Log(x);
        if (Input.GetMouseButtonDown(0))
        {
            ClickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            x = (int)ClickPos.x;
            if (x > 0)
            {
                if (transform.position.x < 1)
                {
                    //move right
                    Vector2 goToNew = new Vector2(transform.position.x + 1, transform.position.y);
                    transform.position = goToNew;

                }
                 
            }
            else
            {
                
                 if (transform.position.x > -1)
                {
                    //move left
                    Vector2 goToNew = new Vector2(transform.position.x - 1, transform.position.y);
                    transform.position = goToNew;
                }
            }
            PreviousClick = x;
            
        }
    }
}

