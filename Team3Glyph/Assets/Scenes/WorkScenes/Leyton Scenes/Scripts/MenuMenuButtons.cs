using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMenuButtons : MonoBehaviour
{
    public void ExitButton()
    {
        Application.Quit();
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("MainGame");
    }
}
