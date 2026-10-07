using System.Collections.Generic;
using UnityEngine;

public class TestCull : MonoBehaviour
{
   // [SerializeField] private int LivesLeft = 3;
    //[SerializeField] private List<GameObject> HealthObjects = new List<GameObject>();
 
    public Spawnballs Spawn;
    [SerializeField]
    private GameObject Effect;
    public GameObject manager;
    private ParticleControler Pcon;
    private int RespawnDelay = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Pcon = Effect.GetComponent<ParticleControler>();
        Spawn = GameObject.Find("Manager").GetComponent<Spawnballs>();
        Spawn = manager.GetComponent<Spawnballs>();
    }
    private void OnTriggerEnter2D(Collider2D obj)
    {
        // Destroy(obj.gameObject);

        obj.gameObject.SetActive(false);
        Pcon.PlayEffect();
        // HealthObjects.Remove(HealthObjects[LivesLeft])

        
        StartCoroutine(StartRespawnCount());

        //Debug.Log("triggered");

    }

    // Update is called once per frame

    System.Collections.IEnumerator StartRespawnCount()
    {
        yield return new WaitForSeconds(RespawnDelay);
        Spawn.Respawn();
    }
}
