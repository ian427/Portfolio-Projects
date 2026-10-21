using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
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
        // Debug.Log("collided");
        if (collision.gameObject.tag == "Player")// checks for tag
        {
            Destroy(gameObject, 0f);
        }
    }
}
