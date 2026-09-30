using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Hunt : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed at which the object moves
    public int Scene;
    private Animator MonsterAnimation;
    public float moveInput;

    private FurnitureData furnitureData;

    public TMP_Text moneyText;
    

    
    public int Money;
    

    void Start()
    {
        furnitureData = (FurnitureData)Resources.Load("GameData");
        string m_Path = Application.dataPath;
        furnitureData.Path = m_Path;
        Debug.Log(m_Path);
        //furnitureData.LoadGameData();

        MonsterAnimation = GetComponent<Animator>();
        //Money= furnitureData.Money ;//SETS furnitur date money as money
    }

    

    // Update is called once per frame
    private void Update()
    {
        moneyText.text = "Gold " + Money + "";

        // Get horizontal input (left and right arrow keys, A/D, etc.)
        float moveInput = Input.GetAxis("Horizontal");

        // Move the GameObject left and right
        transform.Translate(Vector3.right * moveInput * moveSpeed * Time.deltaTime);

        // Flip the GameObject when moving left or right
        if (moveInput > 0) // Moving right
        {
            // Set the localScale's x to positive to face right
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (moveInput < 0) // Moving left
        {
            // Set the localScale's x to negative to face left
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        // If the player is moving, play the walking animation
        if (moveInput != 0)
        {
            MonsterAnimation.SetBool("isWalking", true);
        }
        else
        {
            // If no movement input, stop the walking animation
            MonsterAnimation.SetBool("isWalking", false);
        }

        

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the other object has the "Collectible" tag
        if (other.gameObject.CompareTag("Chest"))
        {
            Destroy(other.gameObject);
            Money = +Random.Range(1, 11);
            //furnitureData.Money = Money;//SETS furnitur date money as money
            string m_Path = Application.dataPath;
            furnitureData.Path = m_Path;
            furnitureData.SaveGameData();

        }

        if (other.gameObject.CompareTag("Home"))
        {
                     
         
         SceneManager.LoadScene("ShopTest");

        }
    }
        

}


