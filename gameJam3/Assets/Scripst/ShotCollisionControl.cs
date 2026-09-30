using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotCollisionControl : MonoBehaviour
{
    public Timer Timer;
    // Start is called before the first frame update
    void Start()
    {
        Timer = GameObject.Find("Manager").GetComponent<Timer>();//find cript on manager
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("hit");
        gameObject.GetComponent<ShotMove>().enabled = false;
         if (gameObject.tag == collision.gameObject.tag)
         {
            Timer.matchSound.Play();
            Destroy(gameObject);
            Timer.TimeLeft = Timer.TimeLeft += 1;
         }
    }
}
