using UnityEngine;

public class celebrationfx : MonoBehaviour
{
    [SerializeField] private GameObject celebrationCanvas;
    [SerializeField] private AudioSource celebrationSound;
    [SerializeField] private AudioSource Backgroundmusic;
    //private bool isTriggered = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        celebrationCanvas.SetActive(false);
    }

    public void PlayCelebration()
    {
        
            celebrationCanvas.SetActive(true);
           
           celebrationSound.Play();
           Backgroundmusic.Stop();
       

    }

}
