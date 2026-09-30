using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class fadeeffect : MonoBehaviour
{
    
    public float FadeDelay = 1f;
    public float AlphaValue = 0;
    public bool StartEnd = false;
    private bool StopEnd = false;
    SpriteRenderer SpriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();

    }
    private void Update()
    {
        if (StartEnd == true)
        {
           StopEnd = true;
           
            StartCoroutine(FadeTo(AlphaValue, FadeDelay));
           // Debug.Log("starting");
        }
        if (StopEnd == true)
        {
            StartEnd = false;
        }
      
    }

   

    private IEnumerator FadeTo(float aValue, float FadeTime)
    {
        float alpha = SpriteRenderer.color.a;
        for (float t = 0.0f; t < 0.004f; t += Time.deltaTime / FadeTime)
        { 
            //Debug.Log(t);
         
            Color NewColor = new Color(SpriteRenderer.color.r, SpriteRenderer.color.g, SpriteRenderer.color.b, Mathf.Lerp(alpha, aValue, t));
            SpriteRenderer.color = NewColor;
            //yield return null;

            yield return null;
        }
       // Debug.Log("breakout");
                 SceneManager.LoadScene("Endscreen");



       
    }
}
