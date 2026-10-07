using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//public class Loadingbar : MonoBehaviour
//{

//    [SerializeField] private GameObject LoadingPanel;
//    [SerializeField] private GameObject LoadingBar;

//    public void Awake()
//    {
//        LoadingPanel.SetActive(false);
//    }

//    public void LoadLevel(string LevelName)
//    {
//        LoadingPanel.SetActive(true);
//        StartCoroutine(LoadLevelAsync(LevelName));
//    }

//    private IEnumerator LoadLevelAsync(string LevelName)
//    {
//        AsyncOperation operation = SceneManager.LoadSceneAsync(LevelName);
//        while (!operation.isDone)
//        {
//            float progress = Mathf.Clamp01(operation.progress);

//            LoadingBar.value = progress;
//            yield return null;
//        }
//    }



//}
