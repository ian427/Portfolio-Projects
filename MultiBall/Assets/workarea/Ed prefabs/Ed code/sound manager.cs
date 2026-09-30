using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class CollisionSound2D : MonoBehaviour
{
    [System.Serializable]
    public class CollisionSound
    {
        public string targetTag;
        public AudioClip sound;
    }

    [Header("Sounds Per Tag")]
    public List<CollisionSound> collisionSounds = new List<CollisionSound>();

    [Header("Audio Settings")]
    public float volume = 1f;
    public float minPitch = 0.9f;
    public float maxPitch = 1.1f;

    private AudioSource audioSource;
    private Dictionary<string, AudioClip> soundLookup;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;

        soundLookup = new Dictionary<string, AudioClip>();

        foreach (var entry in collisionSounds)
        {
            if (!soundLookup.ContainsKey(entry.targetTag) && entry.sound != null)
            {
                soundLookup.Add(entry.targetTag, entry.sound);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (soundLookup.TryGetValue(collision.gameObject.tag, out AudioClip clip))
        {
            audioSource.pitch = Random.Range(minPitch, maxPitch);
            audioSource.PlayOneShot(clip, volume);
        }
    }
}
