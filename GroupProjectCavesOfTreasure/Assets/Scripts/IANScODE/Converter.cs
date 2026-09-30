using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Converter : MonoBehaviour
{
    private ItemManager manager;
    [SerializeField] private int Goldinrecipie;
    [SerializeField] private int Dimondsinrecipie;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("Canvas").GetComponent<ItemManager>();

    }
    public void ConvertToGems ()
    {
        if((0 <= (manager.goldAmount - Goldinrecipie)) &&(0<=(manager.diamondsAmount-Dimondsinrecipie)))
        {
                manager.goldAmount -= Goldinrecipie;
                manager.diamondsAmount -= Dimondsinrecipie;
                manager.mysticGemsAmount++;
        }

    }
}
