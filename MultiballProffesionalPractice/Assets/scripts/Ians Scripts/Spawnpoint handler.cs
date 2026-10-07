using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spawnpointhandler : MonoBehaviour
{
    //public BackgroundTransition background;
    [SerializeField] private List<GameObject> SpawnPoints = new List<GameObject>();
    [SerializeField] private List<Obsticalspawner> obspawn = new List<Obsticalspawner>();
    public float respawnDelay = 5f;
    public float StartSpawnDelay = 2f;
   // [SerializeField] private int spawnAmount = 3;
    public bool CanSpawn = true;
   
    
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    { 
        
        for (int i = 0; i < SpawnPoints.Count; i++)
        {
            obspawn.Add(SpawnPoints[i].GetComponent<Obsticalspawner>());
           
        }
        Restart();
        

    }
    
    private void Restart()
    {
        if (CanSpawn)
        {
            StartCoroutine(Spawn()); 
        }
        
    }
    // Update is called once per frame
    
    IEnumerator Spawn()
    {
        //Debug.Log("SpawnedObstical");

        List<Obsticalspawner> points = new List<Obsticalspawner>(obspawn); 

        for (int i = 0; i < points.Count; ++i)
        {
            //points[i].SetDeactive();

        }
        for (int i = 0; i < points.Count; ++i)
        {
            obspawn[i].SpawnObstacle();

        }
       
        yield return new WaitForSeconds(respawnDelay);
        Restart();
    }
   
}
