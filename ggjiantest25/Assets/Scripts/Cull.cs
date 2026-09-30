using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cull : MonoBehaviour
{
    Timer timer;
    [SerializeField]private string SceneToGoTO;
    void Start()
    {
        timer = GameObject.Find("GameManager").GetComponent<Timer>();//find cript on manager
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {

        Destroy(collision.gameObject);
        if(collision.tag == "Player")
        {
            SceneManager.LoadScene(SceneToGoTO); 
        }
        else if(collision.tag == "Pickup")
        {
            timer.Combo = 0;
        }
        

    }
}
