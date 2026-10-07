using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	// Create a new 'Singleton' instance of this AudioManager class
	public static AudioManager instance;

	[SerializeField] private AudioSource audioSource;

	public static float EffectsVolume
	{
		get => PlayerPrefs.HasKey( "volume_effects" ) ? PlayerPrefs.GetFloat( "volume_effects" ) : 1.0f;
		set => PlayerPrefs.SetFloat( "volume_effects", value );
	}

	public static float MusicVolume
	{
		get => PlayerPrefs.HasKey( "volume_basic" ) ? PlayerPrefs.GetFloat( "volume_music" ) : 1.0f;
		set => PlayerPrefs.SetFloat( "volume_music", value );
	}

	public static float AccessibilityVolume
	{
		get => PlayerPrefs.HasKey( "volume_accessibility" ) ? PlayerPrefs.GetFloat( "volume_accessibility" ) : 1.0f;
		set => PlayerPrefs.SetFloat( "volume_accessibility", value );
	}

	private void Awake()
	{
		// Delete the game object if the 'Singleton' instance has been created and is not our current object, otherwise
		// establish the singleton. 
		if ( instance != null && instance != this )
		{
			Destroy( gameObject );
			return;
		}
		else
		{
			instance = this;
		}

		// Ensure this object doesn't ever get destroyed, that way it passes between scenes
		DontDestroyOnLoad( gameObject );
	}

	public void PlayAudio( AudioClip clip, AudioTypes type )
	{
		if ( clip == null ) return; 

		switch ( type )
		{
			case AudioTypes.Effects:
				audioSource.PlayOneShot( clip, EffectsVolume );
				break;
			case AudioTypes.Music:
				audioSource.PlayOneShot( clip, MusicVolume );
				break;
			case AudioTypes.Accessibility:
				audioSource.PlayOneShot( clip, AccessibilityVolume );
				break;
			default:
				break;
		}
	}
}

public enum AudioTypes
{
	Effects,
	Music,
	Accessibility
}