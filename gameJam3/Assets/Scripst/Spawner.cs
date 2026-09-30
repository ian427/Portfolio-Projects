using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] Ships;
    // Start is called before the first frame update
    void Start()
    {
        int Index = UnityEngine.Random.Range(0, Ships.Length);
        Instantiate(Ships[Index], transform.position, transform.rotation);
    }

    
}
