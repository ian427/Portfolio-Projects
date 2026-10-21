using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    private int Health = 1 ;
    public TMP_Text txtHealth;
    
    // Start is called before the first frame update
    void Start()
    {
        Health = 1;
        txtHealth.text = Health.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
        {
            SceneManager.LoadScene("MainGame");
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug.Log("collided");
        if (collision.gameObject.tag == "Enemy")// checks for tag
        {
            Health -= 1;
            txtHealth.text = Health.ToString();
        }
        if (collision.gameObject.tag == "HealthCollectable")// checks for tag
        {
            Health += 1;
            txtHealth.text = Health.ToString();
        }
    }
}
