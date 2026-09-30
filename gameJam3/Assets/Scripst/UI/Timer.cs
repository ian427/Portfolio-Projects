using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public bool Clock;
    public float TimeLeft;
    public fadeeffect Fade;
    public Text txtTime;
    public int timer = 5;//starting time
    private bool CanEnd = true;
    public AudioSource matchSound;


    // Start is called before the first frame update
    void Start()
    {
        TimeLeft = timer;
        Clock = true;
        Fade = GameObject.Find("fade").GetComponent<fadeeffect>();


    }

    // Update is called once per frame
    void Update()
    {

        if (Clock == true)//prevents couroutine being called every update
        {
            //Debug.Log("mark");
            StartCoroutine("OxGodown");

            Clock = false;
        }
        if ((TimeLeft == 0)||(TimeLeft <= 0))
        {
            TimeLeft = 1;
            Clock = false;
            
            if (CanEnd == true)
            {
                //Debug.Log("fadeing");
                GameObject.Find("PlayerCannon").GetComponent<PlayerMovment>().enabled = false;
                Fade.StartEnd = true; 
                CanEnd = false;
            }
            

            
        }
    }
    private IEnumerator OxGodown()
    {
        yield return new WaitForSeconds(1);//waits 1 second
        TimeLeft = TimeLeft - 1;//decreses meter by 1
        txtTime.text = TimeLeft.ToString();//updates txt

        Clock = true;//resets loop
    }
}
