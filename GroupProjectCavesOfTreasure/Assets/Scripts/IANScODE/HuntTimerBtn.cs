using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HuntTimerBtn : MonoBehaviour
{
    //public AudioSource click;
    private bool canSwitch = false;
    public int CurrentNumberOfHuntsdone;
    public TimeSpan currenttime;
    GameControler gameControler;
    [SerializeField]
    private string SceneToGoTO;
    private int HuntLimit = 5;
    public void Start()
    {

        gameControler = GameObject.Find("Controller").GetComponent<GameControler>();
       


    }
    public void OnButtonPress()
    {
        if (gameControler.lastHuntTime + TimeSpan.FromDays(0.12f) <= TimeSpan.FromTicks(System.DateTime.UtcNow.Ticks))
        {
            if (gameControler.CurrentHuntsDone <= HuntLimit)
            {

                gameControler.CurrentHuntsDone = 0;
                gameControler.lastHuntTime = TimeSpan.FromTicks(System.DateTime.UtcNow.Ticks);
            }
            else if (gameControler.CurrentHuntsDone < HuntLimit)
            {
                gameControler.CurrentHuntsDone = gameControler.CurrentHuntsDone++;
            }
            canSwitch = true;
        }

        if (canSwitch)
        {
            gameControler.PushDataUpdate();
            SceneManager.LoadScene(SceneToGoTO);//remove for sound
            //click.Play();
            canSwitch = true;
            //Debug.Log("click");

        }
        
    }
    
    private void Update()
    {
        /*
        if (!click.isPlaying && canSwitch)//defults true
        {
            Debug.Log("switch");
            SceneManager.LoadScene(SceneToGoTO);//remember to add scene to Build
        }
        */
    }
    
}
