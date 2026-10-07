using System;
using UnityEngine;

public class SparkControler : MonoBehaviour
{
    [SerializeField] private float LifeTime = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(StartRespawnCount());

    }
    System.Collections.IEnumerator StartRespawnCount()
    {
        yield return new WaitForSeconds(LifeTime);
        Destroy(gameObject);       
    }

}
