using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPointControl : MonoBehaviour
{
    private bool CanRun = true;
    private float delayBetweenScales = 10f;
    Vector3 Scaleup = new Vector3(0.01f, 0.01f, 0.01f);

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if(CanRun == true)
        {
            CanRun = false;
            StartCoroutine(ScaleObject());

        }
        

    }
   

    IEnumerator ScaleObject()
    {

        for (int i = 0; i < 30; i++)
        {
            transform.localScale += Scaleup ;
            yield return new WaitForSeconds(delayBetweenScales * Time.deltaTime );
            //Debug.Log("in");
        }
        for (int i = 0; i < 30; i++)
        {
            transform.localScale -= Scaleup ;
            yield return new WaitForSeconds(delayBetweenScales * Time.deltaTime);
            // Debug.Log("out");
        }
        if (CanRun == true)
        {
            StartCoroutine(ScaleObject());

        }
        CanRun = true;
    }
}
