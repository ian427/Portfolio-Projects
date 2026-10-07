using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeInOut : MonoBehaviour
{
    public CanvasGroup canvasgroup;
    public bool fadein = false;
    public bool fadeout = false;
    [SerializeField] public string sceneToGoTo;

    public float TimeToFade;

    // Update is called once per frame
    void Update()
    {
        if(fadein == true)
        {
            if(canvasgroup.alpha < 1)
            {
                canvasgroup.alpha += TimeToFade * Time.deltaTime;
                if(canvasgroup.alpha >= 1)
                {
                    fadein = false;
                    SceneManager.LoadScene(sceneToGoTo);
                }
            }
        }
        if (fadeout == true)
        {
            if (canvasgroup.alpha >= 0)
            {
                canvasgroup.alpha -= TimeToFade * Time.deltaTime;
                if (canvasgroup.alpha <= 0.1)
                {
                    fadeout = false;
                }
            }
        }
    }

    public void FadeIn()
    { fadein = true; }

    public void FadeOut()
    { fadeout = true; }
}
