using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlatformsControler : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private KeyCode One = KeyCode.Alpha1;
    [SerializeField] private KeyCode Two = KeyCode.Alpha2;
    [SerializeField] private KeyCode Three = KeyCode.Alpha3;
    public List<GameObject> Platforms = new List<GameObject>();
    private List<DiagpnalPlatformmovment> PlatfomsScripts = new List<DiagpnalPlatformmovment>();
    private int CurrentPlatform = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (GameObject obj in Platforms)
        {
            PlatfomsScripts.Add(obj.GetComponent<DiagpnalPlatformmovment>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(One))
        {
            PlatfomsScripts[0].selected = true;
            PlatfomsScripts[1].selected = false;
            PlatfomsScripts[2].selected = false;
        }
        if (Input.GetKeyDown(Two))
        {
            PlatfomsScripts[0].selected = false;
            PlatfomsScripts[1].selected = true;
            PlatfomsScripts[2].selected = false;
        }
        if (Input.GetKeyDown(Three))
        {
            PlatfomsScripts[0].selected = false;
            PlatfomsScripts[1].selected = false;
            PlatfomsScripts[2].selected = true;
        }

        /*
        if (Input.GetKeyDown(left))
        {
            if (CurrentPlatform - 1 >= 0)
            {
                PlatfomsScripts[CurrentPlatform].selected = false;
                CurrentPlatform--;
                PlatfomsScripts[CurrentPlatform].selected = true;
            }

        }

        if (Input.GetKeyDown(right))
        {
            if (CurrentPlatform + 1 < Platforms.Count)
            {
                PlatfomsScripts[CurrentPlatform].selected = false;
                CurrentPlatform++;
                PlatfomsScripts[CurrentPlatform].selected = true;
            }
        }
    }
        */

    }
}
