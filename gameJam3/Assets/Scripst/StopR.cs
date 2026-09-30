using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopR : MonoBehaviour
{
    private float Back = 8;

    public void OnCollisionEnter2D(Collision2D collision)
    {
       // Debug.Log("No");
        collision.transform.Translate(Vector3.left * Back * Time.deltaTime);
    }
}
