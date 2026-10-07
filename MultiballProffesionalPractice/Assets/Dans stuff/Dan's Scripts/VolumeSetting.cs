using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
    //The audio mixer and UI sliders
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider musicBar;
    [SerializeField] private Slider sfxBar;
    
    //Starts by loading the music volume or setting it and the sound effects volume
    private void Start()
    {
        if (PlayerPrefs.HasKey("volumeOfMusic"))
        {
            LoadMusicVolume();
        }

        else
        {
            SetMusicVolume();
            SetSFXVolume();
        }
    }

    //Called when the slider is changed and adjusts the volume level
    //The value is also saved
    public void SetMusicVolume()
    {
        float currentVolume = musicBar.value;
        mixer.SetFloat("Music", Mathf.Log(currentVolume) * 20);
        PlayerPrefs.SetFloat("volumeOfMusic", currentVolume);
    }

    //Called when the slider is changed and adjusts the sound effects level
    //The value is also saved
    public void SetSFXVolume()
    {
        float currentVolume = sfxBar.value;
        mixer.SetFloat("SFX", Mathf.Log(currentVolume) * 20);
        PlayerPrefs.SetFloat("volumeOfSFX", currentVolume);
    }

    //A function that is called at the start and the music and sound settings are loaded
    private void LoadMusicVolume()
    {
        musicBar.value = PlayerPrefs.GetFloat("volumeOfMusic");
        sfxBar.value = PlayerPrefs.GetFloat("volumeOfSFX");

        SetMusicVolume();
        SetSFXVolume();
    }

    //Sound scripts created with help from tutorial: https://www.youtube.com/watch?v=G-JUp8AMEx0 
}
