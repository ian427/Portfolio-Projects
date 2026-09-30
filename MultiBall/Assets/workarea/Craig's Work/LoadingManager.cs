using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;



public class LoadingManager : MonoBehaviour
{

    [Header("UI")]
    [SerializeField] Slider loadingSlider;

    [Header("Behavior")]
    [SerializeField] float loadSpeed = 0.6f; //how fast the fake bar fills
    [SerializeField] string menuSceneName = "Main Menu";

    bool isLoadingDone = false;

     void Start()
    {
        Application.targetFrameRate = 60;
        loadingSlider.value = 0f;

        StartCoroutine(routine: FakeLoading());
    }

    IEnumerator FakeLoading()
    {
        while (loadingSlider.value < 1f)
        {
            loadingSlider.value += loadSpeed * Time.deltaTime;
            yield return null;
        }
        isLoadingDone = true;
        SceneManager.LoadScene(menuSceneName);

    
    }


}
