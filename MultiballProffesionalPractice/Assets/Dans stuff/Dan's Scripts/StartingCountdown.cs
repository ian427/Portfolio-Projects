using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartingCountdown : MonoBehaviour
{
    //The start time and present times plus the second counting down
    [SerializeField] private float startTime;
    [SerializeField] private float presentTime;
    [SerializeField] private float secondCounter;

    //Game object components relating to the text of the timer
    [SerializeField] private GameObject startingCanvas;
    [SerializeField] private GameObject timeTextObject;
    [SerializeField] private TMP_Text timeText;
    public Button Skip;
    [HideInInspector] public bool started;

    //Audio sources used
    [SerializeField] private AudioSource secondClick;
    [SerializeField] private AudioSource buzzer;
    [SerializeField] private List<AudioSource> CountDown;
    private bool onTwo;

    //Start modified by a programmer
    void Start()
    {
        started = true;
        //timeTextObject.SetActive(false);
       // StartCoroutine(StartCountDown());
    }
    
    //Initial code in Update typed by me
    //Would subtract from 3 and play the sounds each second
    //Then the buzzer would play once the round starts
    void Update()
    {
        if(started == false)
        {
           
            /*
            presentTime -= Time.unscaledDeltaTime;
            timeText.text = Mathf.Ceil(presentTime).ToString();

            secondCounter -= Time.unscaledDeltaTime;
            if(secondCounter <= 0)
            {
                secondClick.Play();
                secondCounter = 1f;
            }

            if(presentTime <= 0)
            {
                buzzer.Play();
                started = true;
                startingCanvas.SetActive(false);
                Time.timeScale = 1f;
            }
            */
        }
    }

    //Function was created by me but modified by a programmer
    //The commented out lines were my lines although the canvas and textObject lines remain active
    public void StartCountdown()
    {
        //started = false;
        
        startingCanvas.SetActive(true);
        //timeTextObject.SetActive(true);
        // startTime = 3f;
        // secondCounter = 1f;
        //  presentTime = startTime;
        StartCoroutine(StartMyCountDown());
        //secondClick.Play();
    }

    //IEnumerator was added and script modified by one of the programmers to make the function work better
    IEnumerator StartMyCountDown()
    {
        //Debug.Log("3");
        timeText.text = "3";
       CountDown[0].Play();
        yield return new WaitForSecondsRealtime(1f);
        //Debug.Log("2");
        timeText.text = "2";
       CountDown[1].Play();
        yield return new WaitForSecondsRealtime(1f);
       // Debug.Log("1");
        timeText.text = "1";
       CountDown[2].Play();
        yield return new WaitForSecondsRealtime(1f);
        startingCanvas.SetActive(false);
        Time.timeScale = 1f;



    }
}
