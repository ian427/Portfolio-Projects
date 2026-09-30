using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTest2 : MonoBehaviour
{
    public Collider2D Collider;
    // Start is called before the first frame update
    void Start()
    {
        Collider.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
