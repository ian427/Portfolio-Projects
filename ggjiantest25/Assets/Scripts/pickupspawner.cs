using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupspawner : MonoBehaviour
{
    public bool spawn = true;
  
    public GameObject Pickup;
    [SerializeField] private int Pickupinterval = 4;
    [SerializeField] private float delay;
    [SerializeField] private int spawnx;
    [SerializeField] private int spawnHeight = -6;
    private Vector2 SP;
    void Update()
    {
        if (spawn)
        {
            spawn = false;
            StartCoroutine(MoveSpawnPoint());
        }
    }
    private IEnumerator MoveSpawnPoint()
    {
        spawn = false;

        
            yield return new WaitForSeconds(delay); //adds delay
            spawnx = UnityEngine.Random.Range(-1, 2);
            spawnx = (int)spawnx;
            SP = new Vector3(spawnx, spawnHeight); //picks random spot moves to
            transform.position = SP;
            Instantiate(Pickup, transform.position, transform.rotation);

        spawn = true;//resets loop
                     // Debug.Log("moved");
    }
}
