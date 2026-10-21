using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart1 : MonoBehaviour
{
    public AudioSource clipSource;
    public AudioClip clickclip;
    public AudioClip[] clickArray;
    private bool canSwitch = false;
    public void OnButtonPress()
    {
       int index = Random.Range(0, clickArray.Length);
       clickclip = clickArray[index];
       clipSource.clip = clickclip;
        clipSource.Play();
        canSwitch = true;
    }
    private void Update()
    {

       if (!clipSource.isPlaying && canSwitch)//defults true
       { 
          SceneManager.LoadScene("MainScene");
       }
    }
}
