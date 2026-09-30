using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public bool Clock;
    public float Score;

    public Text txtTime;
    public int timer = 5;//starting time
    private bool CanEnd = true;
    public AudioSource matchSound;
    public float Combo = 0.2f;


   // Start is called before the first frame update
   void Start()
    {
        Score = timer;
        Clock = true;



    }

    // Update is called once per frame
    void Update()
    {

        if (Clock == true)//prevents couroutine being called every update
        {
            //Debug.Log("mark");
            StartCoroutine(timeGodown());

            Clock = false;
        }
        if ((Score == 0) || (Score <= 0))
        {
            Score = 0;
            Clock = false;

            if (CanEnd == true)
            {

            }

        }
        IEnumerator timeGodown()
        {
            yield return new WaitForSeconds(1);//waits 1 second
            Score = Score + 1;//incresses meter by 1
            txtTime.text = Score.ToString();//updates txt

            Clock = true;//resets loop
        }
    }
}
