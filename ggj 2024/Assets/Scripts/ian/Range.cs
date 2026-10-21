using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour
{
    public EnemyAi enemyAi;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug.Log("collided");
        if (collision.gameObject.tag == "Player")// checks for tag
        {
            //Debug.Log("collided player");
            enemyAi.ShouldMove();
            
        }
    }
}
