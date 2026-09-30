using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cull : MonoBehaviour
{
    private bool OverFlowed = false;
    private bool CanEnd = false;
    public fadeeffect Fade;

    // Start is called before the first frame update
    void Start()
    {
        Fade = GameObject.Find("fade").GetComponent<fadeeffect>();

    }
    void Update()
    {
       if(CanEnd == true)
       {
            OverFlowed = false;
            Debug.Log("over");
        }
    }

    public void OnTriggerEnter2D()
    {
        //Debug.Log("get out");
        
        OverFlowed = true;
        StartCoroutine("Out");

    }
    public void OnTriggerExit2D()
    {
        OverFlowed = false;
    }
    private IEnumerator Out ()
    {
        yield return new WaitForSeconds(0.6f);
        if (OverFlowed== true)
        {
            GameObject.Find("PlayerCannon").GetComponent<PlayerMovment>().enabled = false;
            CanEnd = true;
            Fade.StartEnd = true;

            
        }
    }
}
