using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerGen : MonoBehaviour
{
    public GameObject[] customerSprite;

    void Start()
    {
        customerSprite = new GameObject[customerSprite.Length];
    }
}
