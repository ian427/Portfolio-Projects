using System.Collections.Generic;
using UnityEngine;

public class Spawnballs : MonoBehaviour
{
    [SerializeField] private GameObject Ball;
    [SerializeField] private float spawnCountdown = 0;
    [SerializeField] private int MaxSpawn, CurrentSpawned = 0;
    [SerializeField] private int EnableSpot = 0;
    [SerializeField] private float speedup = 0.01f;
    [SerializeField] private List<GameObject> Balls = new List<GameObject>();
    private List<Rigidbody2D> rbs = new List<Rigidbody2D>();
    private bool disablespeedup = false;
    public ParticleControler particlesys;
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
    private void SpawnBall ()
    {
        if(CurrentSpawned < MaxSpawn)
        {
            StartCoroutine(SpawnCounter());
        }
    }
    private void Update()
    {
        if ((CurrentSpawned == 3)&&(!disablespeedup))
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
        particlesys.PlayEffect();
        EnableSpot++;
        //Instantiate(Ball, transform.position, transform.rotation);
        CurrentSpawned++;
        yield return new WaitForSeconds(spawnCountdown);
        SpawnBall();
    }
}
