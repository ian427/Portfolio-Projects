using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UniversalButton : MonoBehaviour
{
	[SerializeField] private AudioClip buttonPressAudio;
	[SerializeField] private string sceneToGoTo;
	FadeInOut fade;

    public void Start()
    {
        fade = FindObjectOfType<FadeInOut>();
    }
    public void OnButtonPress()
	{
		AudioManager.instance.PlayAudio( buttonPressAudio, AudioTypes.Effects );
		fade.FadeIn();
	}
}
