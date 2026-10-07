using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LoadingScreenManager : MonoBehaviour
{
    public static LoadingScreenManager Instance;
    public GameObject m_LoadingScreenObject;
    public Slider ProgressBar;


    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

    }

    public void SwitchToScene(int id)
    {
        m_LoadingScreenObject.SetActive(true);
        ProgressBar.value = 0;
        StartCoroutine(SwitchToSceneAsyc(id));
    }

    private IEnumerator SwitchToSceneAsyc(int id)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(id);// calling scene
        while (!asyncLoad.isDone)
        {
            ProgressBar.value = asyncLoad.progress;// progress of the bar to increase
            Debug.Log("aaaaaaa");
            yield return null;
        }
        yield return new WaitForSeconds(0.2f);
        m_LoadingScreenObject.SetActive(false);
        Debug.Log("progress");
    }
}
