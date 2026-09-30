using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private string SceneToGoTO;
    public void OnClickPlay()
    {
        LoadingScreenManager.Instance.SwitchToScene(1);
    }

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(SceneToGoTO);
    }

    public void QuitGame()
    {
       Application.Quit(); 
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
