using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatFormSpawner : MonoBehaviour
{
    public bool spawn = true;
    public GameObject Platform;
    public GameObject Pickup;
    [SerializeField] private int Pickupinterval=4;
    [SerializeField] private float  delay ;
    [SerializeField] private int spawnx;
    [SerializeField] private int spawnHeight= -6;
    private Vector2 SP;
    private float time;

    void Update()
    {
       if (spawn)
        {
            spawn = false;
             spawnx = UnityEngine.Random.Range(-1, 2);
            spawnx = (int)spawnx;
             SP = new Vector3(spawnx, spawnHeight); //picks random spot moves to
            transform.position = SP;
            Instantiate(Platform, transform.position, transform.rotation);
        }
    }

   
}
