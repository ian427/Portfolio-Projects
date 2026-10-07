using System.Collections.Generic;
using UnityEngine;

public class Obsticalspawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> obj = new List<GameObject>();
    [SerializeField] private HealthDown health;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < obj.Count; i++)
        {
            obj[i].SetActive(true);

        }
        for (int i = 0; i < obj.Count; i++)
        {
            obj[i].SetActive(false);

        }
        SpawnObstacle();
        //health = GameObject.Find("Manager").GetComponent<HealthDown>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (transform.position.y <= EndPoint.y)
        {
            //transform.position = StartPoint;
            SpawnObstacle();
        }

        //transform.Translate(Vector2.down * movementSpeed * Time.deltaTime);
    }
    public void SetDeactive()
    {
        for (int i = 0; i < obj.Count; i++)
        {
            obj[i].SetActive(false);

        }
    }
   
    public void SpawnObstacle ()
    {
        for(int i = 0; i < obj.Count;i++)
        {
            obj[i].SetActive(false);
            
        }
         int temp = Random.Range(0, obj.Count);
        if(obj[temp].gameObject.CompareTag("Heart"))
        {
            if (health.LivesLeft < 3)
            {
                temp = Random.Range(0, 8);
                if (temp != 8) { temp = Random.Range(0, obj.Count); }
            }
            else
            {
                SpawnObstacle();
                return;
            }
        }
         obj[temp].SetActive(true);
        
    }
   
    
}
