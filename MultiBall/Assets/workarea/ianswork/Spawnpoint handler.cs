using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spawnpointhandler : MonoBehaviour
{
    //public BackgroundTransition background;
    [SerializeField] private List<GameObject> SpawnPoints = new List<GameObject>();
    [SerializeField] private List<Obsticalspawner> obspawn = new List<Obsticalspawner>();
    public float respawnDelay = 5f;
    [SerializeField] private int spawnAmount = 3;
    public bool CanSpawn = true;
    public enum CurrentBackground
    {
        Default = 0,
        Desert = 1,
        Arctic = 2,
        Jungle = 3,
        Volcano = 4

    }
    
    public CurrentBackground ChosenBackground = CurrentBackground.Default;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    { 
        
        for (int i = 0; i < SpawnPoints.Count; i++)
        {
            obspawn.Add(SpawnPoints[i].GetComponent<Obsticalspawner>());
           
        }
        Restart ();

    }
    public int GetBackground()
    {
        return (int)ChosenBackground;
    }
    private void Restart()
    {
        if (CanSpawn)
        {
            StartCoroutine(Spawn()); 
        }
        
    }
    // Update is called once per frame
    public void UpdateBackground(int Background)
    {

        switch (Background)
        {
            case (int)CurrentBackground.Desert:
                ChosenBackground = CurrentBackground.Desert;
                break;

            case (int)CurrentBackground.Arctic:
                ChosenBackground = CurrentBackground.Arctic;
                break;

            case (int)CurrentBackground.Jungle:
                ChosenBackground = CurrentBackground.Jungle;
                break;
            case (int)CurrentBackground.Volcano:
                ChosenBackground = CurrentBackground.Volcano;
                break;

            default:
                // Code if none of the cases match
                break;
        }
        for (int i = 0; i < obspawn.Count; i++)
        {
            obspawn[i].SetSprites();
        }
    }
    IEnumerator Spawn()
    {
        Debug.Log("SpawnedObstical");

        List<Obsticalspawner> points = new List<Obsticalspawner>(obspawn); 
        for (int i = 0; i < points.Count; i++)
        {
            points[i].SetDeactive();

        }
        for (int i = 0; i < spawnAmount; i++)
        {
            int temp = Random.Range(0, points.Count);
            points[temp].SpawnObstacle();
            points.RemoveAt(temp);
        }
        yield return new WaitForSeconds(respawnDelay);
        Restart();
    }
}
