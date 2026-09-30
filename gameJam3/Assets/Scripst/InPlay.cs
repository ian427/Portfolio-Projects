using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InPlay : MonoBehaviour
{
    private bool empty = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(empty== true)
        {
         Debug.Log("empty");

        }
        
        
    }
    void OnTriggerStay2D(Collider2D other)
    {
        empty = false;
        //Debug.Log("triggered");
    }
   
}
