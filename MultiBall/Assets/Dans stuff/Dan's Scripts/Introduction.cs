using System.Collections;
using UnityEngine;

public class Introduction : MonoBehaviour
{
    [SerializeField] private float startTimeA;
    [SerializeField] private float presentTimeA;
    [SerializeField] private GameObject introTextA;

    [SerializeField] private float startTimeB;
    [SerializeField] private float presentTimeB;
    [SerializeField] private GameObject introTextB;

    [SerializeField] private bool firstIntroActive;
    [SerializeField] private bool lastIntroActive;

    [SerializeField] private GameObject canvas;
    //[SerializeField] private GameObject tapToPlay;
    [SerializeField] private StartingCountdown sc;

    [SerializeField] private bool skipped;

    private Coroutine nextText;
    private Coroutine countdown;

    [SerializeField] private AudioSource voiceLine1;
    [SerializeField] private AudioSource voiceLine2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //OnGameStart();
        }

        if(skipped == true)
        {
            return;
        }

        if(firstIntroActive == true && skipped == false)
        {
            if (skipped == false)
            {
                startTimeA -= Time.unscaledDeltaTime;
                if (startTimeA <= 0)
                {
                    firstIntroActive = false;
                    introTextA.SetActive(false);
                    nextText = StartCoroutine(ActivateNextText());
                }
            }
        }

        if(lastIntroActive == true)
        {
            if(skipped == false)
            {
                startTimeB -= Time.unscaledDeltaTime;
                if (startTimeB <= 0)
                {
                    lastIntroActive = false;
                    introTextB.SetActive(false);
                    countdown = StartCoroutine(ActivateCountdown());
                }
            }
        }
    }

    public void OnGameStart()
    {
        canvas.SetActive(true);
        //tapToPlay.SetActive(false);
        StartCoroutine(beginIntroSequence());
    }

    private IEnumerator beginIntroSequence()
    {
        Time.timeScale = 0;
        yield return null;

        startTimeA = 2f;
        startTimeB = 2f;

        firstIntroActive = true;
        lastIntroActive = false;

        introTextA.SetActive(true);
        introTextB.SetActive(false);

        voiceLine1.Play();
    }

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
        lastIntroActive = false;
        introTextA.SetActive(false);
        introTextB.SetActive(false);

        sc.StartCountdown();
    }
    private IEnumerator ActivateNextText()
    {
        yield return new WaitForSecondsRealtime(1);
        if (skipped == true) yield break;
        lastIntroActive = true;
        introTextB.SetActive(true);
        voiceLine2.Play();
    }

    private IEnumerator ActivateCountdown()
    {
        yield return new WaitForSecondsRealtime(1);
        if (skipped == true) yield break;
        sc.StartCountdown();
    }
}
