using UnityEngine;
using UnityEngine.SceneManagement;
public class PuaseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    bool isPaused = false;
    public void Pause()
    {

        isPaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Home()
    {
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1;
    }

    public void Resume()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Paused");
            if (!isPaused)
            {
                Pause();
            }

            else
                Resume();

        }
    }
}
