using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public enum AbilityType
{
    Attack,
    Defence,
}


public class Gamemode : MonoBehaviour
{

    public GameObject abilityHolder;

    public GameObject playerPrefab;

    private GameObject playerObj;
    public GameObject[] enemies;
    public GameObject enemyObj;

    public GameObject playerPoint, enemyPoint;

    public GameObject WinScreen, DeathScreen;
    public GameObject StartCanvas;

    int turnNum;

    bool playerTurn = false;

    List<Ability> abilityQueueList;


    private AbilityManager playerAbilityManager;
    private AbilityManager aiAbilityManager;

    private List<GameObject> abilityButtons;

    [SerializeField]
    private TMP_Text playerAbilityText, aiAbilityText;
    [SerializeField]
    private TMP_Text hitMissText;
    [SerializeField]
    private TMP_Text winText;
    [SerializeField]
    private TMP_Text deadText;

    private int fightNumber = 0;

    bool betweenFights = true; 

    private void Start()
    {
        NewGame();
    }


    void NewGame()
    {

        StartCanvas.SetActive(true);

        hitMissText.SetText("");


        if (!playerObj)
        {
            playerObj = Instantiate(playerPrefab, playerPoint.transform.position, Quaternion.identity);
        }

        playerObj.GetComponent<Health>().AddHealth(100000000);

        SpawnNewEnemy();

        playerTurn = true;

        if (!playerAbilityManager)
        {
            playerAbilityManager = playerObj.GetComponent<AbilityManager>();
        }

        aiAbilityManager = enemyObj.GetComponent<AbilityManager>();

        SetAbilityText(null, null, playerAbilityText, true);
        SetAbilityText(null, null, aiAbilityText, true);


        abilityButtons = new List<GameObject>();

        if (abilityHolder.transform.childCount <= 0)
        {
            //Spawn buttons
            StartCoroutine(SpawnButtons());
        }


        //bad code :(
        Ability[] abilities_temp = FindObjectsByType<Ability>(FindObjectsSortMode.None);
       
        foreach(Ability a in abilities_temp)
        {
            abilityButtons.Add(a.gameObject);
        }

        abilityQueueList = new List<Ability>();

        WinScreen.SetActive(false);
        DeathScreen.SetActive(false);

        betweenFights = false;
    }

    void SpawnNewEnemy()
    {
        // PlayerPrefs.SetInt("Bossnumber", 3);
        //  PlayerPrefs.Save();

        // enemyObj = enemies[PlayerPrefs.GetInt("Bossnumber")];
        var a = enemies[fightNumber];

        enemyObj = Instantiate(a, enemyPoint.transform.position, Quaternion.identity);
    }


    IEnumerator SpawnButtons()
    {

        foreach (GameObject obj in playerAbilityManager.abilityButtons)
        {          
            Instantiate(obj, abilityHolder.transform);
            abilityButtons.Add(obj);

        }

        yield return new WaitForSeconds(0.1f);

        foreach (GameObject obj in abilityButtons)
        {
          obj.GetComponent<Button>().interactable = false;
        }

    }


 

    public void StartFight()
    {
       fightNumber++;
       StartPlayerTurn();
    }
    

    void StartPlayerTurn()
    {
        if (!betweenFights)
        {


            playerTurn = true;
            //enable buttons on ui
            foreach (GameObject obj in abilityButtons)
            {
                obj.GetComponent<Button>().interactable = true;
            }


            //clear ability queue
            abilityQueueList.Clear();

            SetAbilityText(null, null, playerAbilityText, true);
        }
    }

    void EndTurn()
    {
        //disable buttons on ui
        foreach (GameObject obj in abilityButtons)
        {
            obj.GetComponent<Button>().interactable = false;
        }

        //execute ability queue
        StartCoroutine(ExecuteAbilityQueue());
        

    }

    IEnumerator StartAiTurn()
    {
        if (!betweenFights)
        {

            SetAbilityText(null, null, aiAbilityText, true);
            yield return new WaitForSeconds(1f);
            abilityQueueList.Clear();

            //similar to player but no buttons - ai decides move
            var abilityNum = UnityEngine.Random.Range(0, aiAbilityManager.abilityButtons.Count);
            AddActionToQueue(aiAbilityManager.abilityButtons[abilityNum].GetComponent<Ability>());
            yield return new WaitForSeconds(2f);
            SetAbilityText(null, null, aiAbilityText, true);
        }
    }

    public void AddActionToQueue(Ability ability)
    {
        abilityQueueList.Add(ability);
        EndTurn();

    }


    IEnumerator ExecuteAbilityQueue()
    {
        
        foreach (Ability ability in abilityQueueList)
        {
           // Debug.Log("Executing ability: " + ability.abilityName);
            //Execute ability logic here

            //get ability data
            //calculate hit

            if(playerTurn)
            {
               Debug.Log("Player is using ability: " + ability.abilityName);
                SetAbilityText(playerObj, ability, playerAbilityText, false);
            }

            else
            {
                Debug.Log("Ai is using ability: " + ability.abilityName);
                SetAbilityText(enemyObj, ability, aiAbilityText, false);
            }


            if (ability.abilityType == AbilityType.Attack)
            {
                var mod = UnityEngine.Random.Range(ability.hitChanceMin, ability.hitChanceMax);

                var hitRoll = UnityEngine.Random.Range(0, 100);


                //  Debug.Log("modifier: " + mod + " " + "hitrool: " + hitRoll + " " + "if hitroll is greater than " + (ability.hitChanceMax / 100) * mod);
                Debug.Log("hitrool: " + hitRoll + " " + "modifier: " + mod);

                if (playerTurn)
                {
                   hitRoll += enemyObj.GetComponent<Health>().GetShield();
                    if(enemyObj.GetComponent<Health>().GetShield() > 0)
                    {
                        enemyObj.GetComponent<Health>().DamageShield();
                    }
                }

                else
                {
                    hitRoll += playerObj.GetComponent<Health>().GetShield();
                    if (playerObj.GetComponent<Health>().GetShield() > 0)
                    {
                        playerObj.GetComponent<Health>().DamageShield();
                    }
                }


                if (hitRoll <= mod) //example hit threshold
                {

                    var damage = UnityEngine.Random.Range(ability.damageRangeMin, ability.damageRangeMax);
                    Debug.Log("Ability hit! Dealt " + damage + " damage.");
                    hitMissText.SetText("Hit for " + damage);

                    if (playerTurn)
                    {
                        //apply damage to enemy
                        enemyObj.GetComponent<Health>().TakeDamage(damage);
                        //Debug.Log("Enemy takes " + damage + " damage.");
                    }
                    else
                    {
                        //apply damage to player
                        playerObj.GetComponent<Health>().TakeDamage(damage);
                        // Debug.Log("Player takes " + damage + " damage.");
                    }
                }

                else
                {
                    hitMissText.SetText("Missed!");
                    Debug.Log("Ability missed!");
                }
            }

            if(ability.abilityType == AbilityType.Defence)
            {
                if (playerTurn)
                {
                    Debug.Log("Defence ability used.");
                    playerObj.GetComponent<Health>().AddShield(ability.shieldAmount);
                    Debug.Log(ability.shieldAmount);
                }

                else
                {
                    Debug.Log("Defence ability used.");
                    enemyObj.GetComponent<Health>().AddShield(ability.shieldAmount);
                    Debug.Log(ability.shieldAmount);
                }
            }


            //apply damage/effects
        }
       
        yield return new WaitForSeconds(1f);

        hitMissText.SetText("");

        if (playerTurn)
        {
            playerTurn = false;
            StartCoroutine(StartAiTurn());
        }

        else
            StartPlayerTurn();


    }

    private void SetAbilityText(GameObject character, Ability ability, TMP_Text textObj, bool blank = false)
    {
        if (blank)
        {
            textObj.SetText("");
        }
        else
        {

            textObj.SetText(character.GetComponent<CharacterStats>().characterName.ToString() + " used: " + ability.abilityName);
            

        }
    }




    public IEnumerator PlayerWin()
    {
        betweenFights = true;
        WinScreen.SetActive(true);
        //play unmask animation
        winText.SetText("You Unmasked " + enemyObj.GetComponent<CharacterStats>().characterName);

        playerAbilityManager.AddAbility(enemyObj.GetComponent<AbilityManager>().abilityButtons[enemyObj.GetComponent<AbilityManager>().abilityButtons.Count - 1]);

        Instantiate(enemyObj.GetComponent<AbilityManager>().abilityButtons[enemyObj.GetComponent<AbilityManager>().abilityButtons.Count - 1], abilityHolder.transform);
        abilityButtons.Add(enemyObj.GetComponent<AbilityManager>().abilityButtons[enemyObj.GetComponent<AbilityManager>().abilityButtons.Count - 1]);

        yield return new WaitForSeconds(2f);

        //Destroy(playerObj);
        Destroy(enemyObj);


        NewGame();

        yield return new WaitForSeconds(1.5f);


        WinScreen.SetActive(false);
        winText.SetText("");




    }

    public IEnumerator PlayerDeath()
    {
        DeathScreen.SetActive(true);
        //play death animation
        deadText.SetText("You were slain by " + enemyObj.GetComponent<CharacterStats>().characterName);

        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Main Menu");
    }

}
