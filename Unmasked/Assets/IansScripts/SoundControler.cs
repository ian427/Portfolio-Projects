using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundControler : MonoBehaviour
{
    AudioSource Source;
    public List<AudioClip> Soundfx;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Source = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
   public void PlaySound(string ClipName)
    {

        if (Source == null)
        {
            Debug.LogWarning("SoundControler: AudioSource is missing.");
            return;
        }
        if (Soundfx == null || Soundfx.Count == 0)
        {
            Debug.LogWarning("SoundControler: Soundfx list is empty.");
            return;
        }
        if (string.IsNullOrEmpty(ClipName))
        {
            Debug.LogWarning("SoundControler: clipName is null or empty.");
            return;
        }
        int index = -1;
        for (int i = 0; i < Soundfx.Count; i++)
        {
            if (Soundfx[i].name == ClipName)
            {
                index = i;
                break;
            }  
        }
        if (index != -1)
        {
           Source.PlayOneShot(Soundfx[index]);
        }
        else
        {
            Debug.LogWarning("SoundControler: ClipName " + ClipName + " not found in Soundfx list.");
            return;
        }

    }
}
