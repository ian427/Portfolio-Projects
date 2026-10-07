using UnityEngine;

public class iansPause : MonoBehaviour
{
    [SerializeField] private KeyCode pause = KeyCode.P;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    bool isPaused = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(pause))
        {
            Pause();

        }
    }
    public void Pause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}
