using System.Collections.Generic;
using UnityEngine;

public class Obsticalspawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> obj = new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < obj.Count; i++)
        {
            obj[i].SetActive(true);

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetDeactive()
    {
        for (int i = 0; i < obj.Count; i++)
        {
            obj[i].SetActive(false);

        }
    }
    public void SetSprites ()
    {
        for (int i = 0; i < obj.Count; i++)
        {
            obj[i].GetComponent<ObsticalHandler>().UpdateSprite();

        }
       
    }
    public void SpawnObstacle ()
    {
        for(int i = 0; i < obj.Count;i++)
        {
            obj[i].SetActive(false);
            
        }
         int temp = Random.Range(0, obj.Count);
         obj[temp].SetActive(true);
        obj[temp].GetComponent<ObsticalHandler>().UpdateSprite();
    }
   
    
}
