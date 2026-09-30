using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class regularPoints : MonoBehaviour
{
    public ScoreDisplay Points;
    // Start is called before the first frame update
    void Start()
    {
        Points = GameObject.Find("Manager").GetComponent<ScoreDisplay>();//find cript on manager

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnCollisionEnter2D(Collision2D collision)
    {

        if (gameObject.tag == collision.gameObject.tag)
        {

            Points.score += 2;
        }
    }
}
