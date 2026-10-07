using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndButtonFix : MonoBehaviour
{
    [SerializeField] private AudioClip buttonPressAudio;
    FadeInOut fade;
    // Start is called before the first frame update
    void Start()
    {
        fade = FindObjectOfType<FadeInOut>();
    }

    // Update is called once per frame
    public void OnButtonPress()
    {
        fade.sceneToGoTo = "Menu";
        AudioManager.instance.PlayAudio(buttonPressAudio, AudioTypes.Effects);
        fade.FadeIn();
    }
}
