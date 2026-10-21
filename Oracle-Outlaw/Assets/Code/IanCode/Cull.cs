using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cull : MonoBehaviour
{
    Camera cam;
    public float Height;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        ScreenSize();
       // Debug.Log("Spawned");
    }
    void ScreenSize()
    {
        
        Height = 1 / (cam.WorldToViewportPoint(new Vector3(1, 1, 0)).y - .5f);

    }
    // Update is called once per frame
    void Update()
    {
        
        
        Vector3 randomSP = new Vector3(0,(Height / 2) / -1 - (1) , 0);
        transform.position = randomSP;

    }

    public void OnTriggerEnter2D (Collider2D collision)
    {
        // Debug.Log("hit");
        if (collision.gameObject.tag == "Enemy" )
        {
            //If the GameObject has the same tag as specified, output this message in the console
           // Debug.Log("Dead");
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Collectable")
        {
            //If the GameObject has the same tag as specified, output this message in the console
           // Debug.Log("Dead");
            Destroy(collision.gameObject);
        }
    }
}
