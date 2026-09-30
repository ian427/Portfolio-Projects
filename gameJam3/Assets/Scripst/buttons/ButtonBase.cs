using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ButtonBase : MonoBehaviour
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
            Debug.Log("switch");
            SceneManager.LoadScene("Level1");
        }
    }
}
