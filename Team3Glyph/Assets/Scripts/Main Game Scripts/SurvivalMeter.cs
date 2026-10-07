using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SurvivalMeter : MonoBehaviour
{
    public static float meterRemaining = 1.0f;

	[SerializeField] private Slider meter;
	[SerializeField] private string sceneToGoTo;
	[SerializeField] private float decreaseSpeed = 0.1f;
	[SerializeField] private AudioClip endOfPlayAudio;
    FadeInOut fade;

    // Start is called before the first frame update
    void Start()
    {
        fade = FindObjectOfType<FadeInOut>();
        meterRemaining = 1.0f;
    }

    
    // Update is called once per frame
    void Update()
    {
        meterRemaining -= decreaseSpeed * Time.deltaTime;
        meter.value = meterRemaining;
        if (meterRemaining <= 0.0f)
        {
			ScoreKeeper.UpdateHighScore();
			RareGlyphTracker.SaveRareGlyphs();
			AudioManager.instance.PlayAudio( endOfPlayAudio, AudioTypes.Effects );
            fade.FadeIn();
            meterRemaining = 1.0f;
        }
    }

	public static void ResetMeter()
	{
		meterRemaining = 1.0f;
	}
}
