using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopL : MonoBehaviour
{
    private float Back = 8;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("No");
        collision.transform.Translate(Vector3.right * Back * Time.deltaTime);
    }
}
//https://www.youtube.com/watch?v=5GWj2LS4d8s
