using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpControl : MonoBehaviour
{
    public Timer Timer;
    [SerializeField] private float movementSpeed = 0.1f;
    void Awake()
    {
        Timer = GameObject.Find("GameManager").GetComponent<Timer>();//find cript on manager
    }
    void Update()
    {
        transform.Translate(Vector3.up * movementSpeed * Time.deltaTime);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {

        //Debug.Log("hit");
        
        if (collision.gameObject.tag == "Player")
        {
            
            Destroy(this.gameObject);
            Timer.Combo += 0.1f;
            Timer.Score = Timer.Score += 15 * Timer.Combo  ;
        }
    }
}
