using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class Hunt2 : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed at which the object moves
    public int Scene;
    private Animator MonsterAnimation;
    public float moveInput;
    private bool hasOpenedChest;

    private FurnitureData furnitureData;
    public GameObject popUpMenu;
    [SerializeField] private GameManager gameManager;

    public TMP_Text goldText;
    public TMP_Text diamondsText;
    public TMP_Text gemsText;

    public GameObject rewardTextImage;
    public TMP_Text rewardText;

    [HideInInspector] public int Gold;
    [HideInInspector] public int Diamonds;
    [HideInInspector] public int Gems;

    [HideInInspector] public int rewardGold;
    [HideInInspector] public int rewardDiamonds;
    [HideInInspector] public int rewardGems;
    [HideInInspector] public int percentage;

    public int reward1;
    public int reward2;

    public TMP_Text reward1Text;
    public TMP_Text reward2Text;

    public string reward1Name;
    public string reward2Name;

    public GameObject goldImage1;
    public GameObject goldImage2;
    public GameObject diamondImage1;
    public GameObject diamondImage2;
    public GameObject gemsImage1;
    public GameObject gemsImage2;

    //SFX
    public AudioSource chestOpenSFX;
    public AudioSource itemSelectSFX;

    //VFX
    public ParticleSystem goldVFX;
    public ParticleSystem diamondVFX;
    public ParticleSystem gemsVFX;
    public GameObject walkVFX;

    private void Start()
    {
        furnitureData = (FurnitureData)Resources.Load("GameData");

        hasOpenedChest = false;

        //furnitureData.LoadGameData();
        popUpMenu.SetActive(false);
        walkVFX.SetActive(false);

        rewardTextImage.SetActive(false);
        //percentage = Random.Range(0, 100);

        MonsterAnimation = GetComponent<Animator>();
        Gold = 0 ;//SETS furnitur date money as money
        Diamonds = 0 ;//SETS furnitur date money as money
        Gems = 0;//SETS furnitur date money as money

        goldImage1.SetActive(false);
        goldImage2.SetActive(false);
        diamondImage1.SetActive(false);
        diamondImage2.SetActive(false);
        gemsImage1.SetActive(false);
        gemsImage2.SetActive(false);
    }

    private void Update()
    {
        goldText.text = "Gold " + Gold + "";
        diamondsText.text = "Diamonds " + Diamonds + "";
        gemsText.text = "Mystic Gems " + Gems + "";

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
            walkVFX.SetActive(true);
        }
        else
        {
            // If no movement input, stop the walking animation
            MonsterAnimation.SetBool("isWalking", false);
            walkVFX.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the other object has the "Collectible" tag
        if (other.gameObject.CompareTag("Chest"))
        {
            //StartCoroutine(openChest());
            //Destroy(other.gameObject);
            

            //Money = +Random.Range(1, 11);
            //furnitureData.Money = Money;//SETS furnitur date money as money

            //string m_Path = Application.dataPath;
            //furnitureData.Path = m_Path;
            //furnitureData.SaveGameData();
        }

        if (other.gameObject.CompareTag("Home"))
        {

            SaveData();
            SceneManager.LoadScene("ShopScene");

        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Chest"))
        {
            if (hasOpenedChest == true)
            {
                //Destroy(collision.gameObject);
                //hasOpenedChest = false;
            }
        }
    }

    public void RandomizeFirstRewards()
    {
        if(percentage <= 64)
        {
            rewardGold = +Random.Range(1, 11);
            reward1 = rewardGold;
            reward1Name = "Gold";
            goldImage1.SetActive(true);
            diamondImage1.SetActive(false);
            gemsImage1.SetActive(false);
            reward1Text.text = " " + reward1;
        }

        if(percentage >= 65 && percentage < 98)
        {
            rewardDiamonds = +Random.Range(3, 7);
            reward1 = rewardDiamonds;
            reward1Name = "Diamonds";
            goldImage1.SetActive(false);
            diamondImage1.SetActive(true);
            gemsImage1.SetActive(false);
            reward1Text.text = " " + reward1;
        }

        if(percentage == 98 || percentage == 99 || percentage == 100)
        {
            rewardGems = +Random.Range(1, 4);
            reward1 = rewardGems;
            reward1Name = "Gems";
            goldImage1.SetActive(false);
            diamondImage1.SetActive(false);
            gemsImage1.SetActive(true);
            reward1Text.text = " " + reward1;
        }
    }

    public void RandomizeSecondRewards()
    {
        if (percentage <= 64)
        {
            rewardGold = +Random.Range(1, 11);
            reward2 = rewardGold;
            reward2Name = "Gold";
            goldImage2.SetActive(true);
            diamondImage2.SetActive(false);
            gemsImage2.SetActive(false);
            reward2Text.text = " " + reward2;
        }

        if (percentage >= 65 && percentage < 98)
        {
            rewardDiamonds = +Random.Range(3, 7);
            reward2 = rewardDiamonds;
            reward2Name = "Diamonds";
            goldImage2.SetActive(false);
            diamondImage2.SetActive(true);
            gemsImage2.SetActive(false);
            reward2Text.text = " " + reward2;
        }

        if (percentage == 98 || percentage == 99 || percentage == 100)
        {
            rewardGems = +Random.Range(1, 4);
            reward2 = rewardGems;
            reward2Name = "Gems";
            goldImage2.SetActive(false);
            diamondImage2.SetActive(false);
            gemsImage2.SetActive(true);
            reward2Text.text = " " + reward2;
        }
    }

    public void SelectFirstRewards()
    {
        if(reward1Name == "Gold")
        {
            rewardTextImage.SetActive(true);
            StartCoroutine(disableText());
            rewardText.text = "Gold obtained: " + reward1;
            Gold += reward1;
            goldVFX.Play();
        }

        if (reward1Name == "Diamonds")
        {
            rewardTextImage.SetActive(true);
            StartCoroutine(disableText());
            rewardText.text = "Diamonds obtained: " + reward1;
            Diamonds += reward1;
            diamondVFX.Play();
        }

        if (reward1Name == "Gems")
        {
            rewardTextImage.SetActive(true);
            StartCoroutine(disableText());
            rewardText.text = "Gems obtained: " + reward1;
            Gems += reward1;
            gemsVFX.Play();
        }

        itemSelectSFX.Play();
        moveSpeed = 5f;
        hasOpenedChest = true;
    }

    public void SelectSecondRewards()
    {
        if (reward2Name == "Gold")
        {
            rewardTextImage.SetActive(true);
            StartCoroutine(disableText());
            rewardText.text = "Gold obtained: " + reward2;
            Gold += reward2;
            goldVFX.Play();
        }

        if (reward2Name == "Diamonds")
        {
            rewardTextImage.SetActive(true);
            StartCoroutine(disableText());
            rewardText.text = "Diamonds obtained: " + reward2;
            Diamonds += reward2;
            diamondVFX.Play();
        }

        if (reward2Name == "Gems")
        {
            rewardTextImage.SetActive(true);
            StartCoroutine(disableText());
            rewardText.text = "Gems obtained: " + reward2;
            Gems += reward2;
            gemsVFX.Play();
        }

        itemSelectSFX.Play();
        moveSpeed = 5f;
        hasOpenedChest = true;
    }

    public void SaveData()
    {
        furnitureData.Gold += Gold;

        furnitureData.Diamond += Diamonds;

        furnitureData.Gem += Gems;
    }
    
    public void ClickOnChest()
    {
        StartCoroutine(openChest());
        chestOpenSFX.Play();
        moveSpeed = 0;
    }

    public IEnumerator openChest()
    {
        yield return new WaitForSeconds(2);
        popUpMenu.SetActive(true);
        percentage = Random.Range(0, 100);
        RandomizeFirstRewards();
        percentage = Random.Range(0, 100);
        RandomizeSecondRewards();
    }
    public void ResetMoneyForTestReasons()
    {
        furnitureData.Gold = 10;
        furnitureData.Diamond = 5;
        furnitureData.Gem = 2;
    }

    private IEnumerator disableText()
    {
        yield return new WaitForSeconds(1);
        rewardTextImage.SetActive(false);
    }
}
