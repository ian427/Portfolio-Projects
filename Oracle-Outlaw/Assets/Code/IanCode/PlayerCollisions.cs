using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollisions : MonoBehaviour
{
    public ScoreDisplay collectScore;
    public AudioSource coinCollected;


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
        //Debug.Log("hitC");
        if (collision.gameObject.tag == "Collectable")
        {
            //If the GameObject has the same tag as specified, output this message in the console
           // Debug.Log("Point");
            collectScore.score += 30;
            coinCollected.Play();
            Destroy(collision.gameObject);

        }
        //Debug.Log("hitD");
        if (collision.gameObject.tag == "Enemy")
        {
            //If the GameObject has the same tag as specified, output this message in the console
           // Debug.Log("Dead");
            SceneManager.LoadScene("EndScreen");

        }



    }

}
