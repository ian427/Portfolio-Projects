using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class spawnnow : MonoBehaviour
{
    PlatFormSpawner timer;
    [SerializeField] private string SceneToGoTO;
    void Start()
    {
        timer = GameObject.Find("Spawner").GetComponent<PlatFormSpawner>();//find cript on manager
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Platform")
            timer.spawn = true;


    }
}
