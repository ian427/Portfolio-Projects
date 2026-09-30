using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class CombatAreaController : MonoBehaviour
{
    public int EnemiesInArea;
    public List<GameObject> Buildings = new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
  
    public void RemoveCount ()
    {
        EnemiesInArea--;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            EnemiesInArea++;
        }
        if (other.CompareTag("Building"))
        {
            Buildings.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemiesInArea--;
        }
    }
}
