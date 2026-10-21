using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public float rotateSpeed = 0.1f;
    

    // Update is called once per frame
    void Update()
    {
        //rotates game object
        transform.Rotate (0f,0f,rotateSpeed*Time.deltaTime,Space.Self);
    }
}
