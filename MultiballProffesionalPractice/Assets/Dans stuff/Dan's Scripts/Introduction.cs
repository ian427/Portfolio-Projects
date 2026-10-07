using System.Collections;
using UnityEngine;

public class Introduction : MonoBehaviour
{
    //The values and objects relating to the starting time and countdown of the first UI text
    [SerializeField] private float startTimeA;
    [SerializeField] private float presentTimeA;
    [SerializeField] private GameObject introTextA;

    //Bools to see which intros are active or not
    [SerializeField] private bool firstIntroActive;

    //Used for the computer version
    [SerializeField] private GameObject canvas;
    //[SerializeField] private GameObject tapToPlay;
    [SerializeField] private StartingCountdown sc;

    //A bool for skipping the intro
    [SerializeField] private bool skipped;

    private Coroutine nextText;
    private Coroutine countdown;

    //Starts by setting the tapToPlay overlay as true and the regular canvas as false. The time scale is stopped
    void Start()
    {
        skipped = false;
        //tapToPlay.SetActive(true);
        canvas.SetActive(false);
        Time.timeScale = 0;
        OnGameStart();
    }

    // Update is called once per frame
    void Update()
    {
        //If the skipped value is false, then the countdown will begin which will display the first intro text while waiting for the second one to start
        if(firstIntroActive == true && skipped == false)
        {
            if (skipped == false)
            {
                startTimeA -= Time.unscaledDeltaTime;
                if (startTimeA <= 0)
                {
                    firstIntroActive = false;
                    introTextA.SetActive(false);
                    nextText = StartCoroutine(ActivateCountdown());
                    canvas.SetActive(false);
                    Time.timeScale = 1;
                }
            }
        }

        //Loads the second intro text and begins the next countdown that shows the other text
    }

    //Function called when the game begins, such as starting the text countdown
    public void OnGameStart()
    {
        canvas.SetActive(true);
        //tapToPlay.SetActive(false);
        StartCoroutine(beginIntroSequence());
    }

    //
    private IEnumerator beginIntroSequence()
    {
        Time.timeScale = 0;
        yield return null;

        startTimeA = 2f;

        firstIntroActive = true;

        introTextA.SetActive(true);
    }

    //A void that is called when a button is pressed. It will skip the intro sequence. 
    public void SkipIntros()
    {
        skipped = true;

        if (nextText != null)
        {
            StopCoroutine(nextText);
        }

        if (countdown != null)
        {
            StopCoroutine(countdown);
        }

        firstIntroActive = true;
        introTextA.SetActive(false);
        canvas.SetActive(false);
        Time.timeScale = 1;
    }

    private IEnumerator ActivateNextText()
    {
        yield return new WaitForSecondsRealtime(1);
        if (skipped == true) yield break;
    }

    //An IEnumerator that will begin the 321 countdown after the final intro text id displayed
    private IEnumerator ActivateCountdown()
    {
        yield return new WaitForSecondsRealtime(1);
        if (skipped == true) yield break;
    }

    //AI assistance was used to help make adjustments so the script could function properly
}
