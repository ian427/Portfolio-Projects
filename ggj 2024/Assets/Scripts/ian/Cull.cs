using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cull : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug.Log("hit");
        if (collision.gameObject.tag == "Astroid")
        {
            //If the GameObject has the same tag as specified, output this message in the console
            // Debug.Log("Dead");
            Destroy(collision.gameObject);
        }
        Destroy(collision.gameObject); // detroys any object on collision
    }
}
