using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerCuller : MonoBehaviour
{
    [SerializeField] private string SceneToGoTO;
    public void OnTriggerEnter2D(Collider2D collision)
    {

        
        if (collision.tag == "Player")
        {
            Debug.Log("Are you real");
            SceneManager.LoadScene(SceneToGoTO);
            Destroy(collision.gameObject); 
        }


    }
}
