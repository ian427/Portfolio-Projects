using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitButton : MonoBehaviour
{
    
    public AudioSource click;
    private bool canSwitch = false;
    public void OnButtonPress()
    {
        click.Play();

        canSwitch = true;

        Debug.Log("click");
    }
    private void Update()
    {

        if (!click.isPlaying && canSwitch)//defults true
        {
            Application.Quit();
            Debug.Log("Quit!");
        }
    }


}

