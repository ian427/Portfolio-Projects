using System.Collections.Generic;
using UnityEngine;

public class Spawnballs : MonoBehaviour
{
    //[SerializeField] private GameObject Ball;
    [SerializeField] private float spawnCountdown = 0;
    [SerializeField] private int MaxSpawn = 3;
    public int CurrentSpawned = 0;
    [SerializeField] private int EnableSpot = 0;
    [SerializeField] private float speedup = 0.01f;
    [SerializeField] private float SpawnHeight = 0f;
    [SerializeField] private List<GameObject> Balls = new List<GameObject>();
    private List<Rigidbody2D> rbs = new List<Rigidbody2D>();
    private bool disablespeedup = false;
    //public ParticleControler particlesys;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Rigidbody2D rb;
    void Start()
    {
        SpawnBall();
        for (int i = 0; i < Balls.Count; i++)
        {
            rbs.Add(Balls[i].GetComponent<Rigidbody2D>());
        }

    }
    public void SpawnBall()
    {
        if (CurrentSpawned < MaxSpawn)
        {
            StartCoroutine(SpawnCounter());
        }
        else if(EnableSpot == 3)
        {
            Respawn();
        }
    }
    
         public void Respawn()
         {
            if (EnableSpot == MaxSpawn)
            {
                for (int i = 0; i < MaxSpawn; i++)
                {
                    if (!Balls[i].gameObject.activeInHierarchy)
                    {
                        
                        Balls[i].transform.position = new Vector3(transform.position.x, transform.position.y, 0);
                       
                        Balls[i].SetActive(true);
                   
                    //CurrentSpawned++;
                }


                }
            }

         }
    
    private void Update()
    {
        if ((EnableSpot == 3)&&(!disablespeedup))
        {
            for (int i = 0; i < Balls.Count; i++)
            {
                if(rbs[i].gravityScale > 1)
                {
                    if(rbs[i] != null)
                    {
                        disablespeedup = true;
                        rbs[i].gravityScale = 1;
                    }
                   
                }
                else 
                {
                    rbs[i].gravityScale += speedup;

                }
               

            }
        }
    }
    System.Collections.IEnumerator SpawnCounter ()
    {
        Balls[EnableSpot].SetActive(true);
       // particlesys.PlayEffect();
        EnableSpot++;
        //Instantiate(Ball, transform.position, transform.rotation);
        CurrentSpawned++;
        yield return new WaitForSeconds(spawnCountdown);
        SpawnBall();
    }
    
    public void Freezeballs()
    {
        for(int i = 0;i < rbs.Count;i++)
        {
            //rbs[i].constraints = RigidbodyConstraints2D.FreezePosition;
            Balls[i].SetActive(false);
        }
    }
}
