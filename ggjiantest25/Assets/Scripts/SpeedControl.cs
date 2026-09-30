using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpeedControl : MonoBehaviour
{
    [SerializeField] public float movementSpeed = 0.5f;
    private float time;
    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = 0.5f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
        if (movementSpeed >= 20)
        {
            movementSpeed = 20;
        }
        else 
        {
           
            movementSpeed += 0.003f;
        }
       
        
    }
}
