using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Timer;
    private int Minutes;
    private int Seconds;
    [SerializeField] private int Total;
    public bool Stopclock = false;
    public BackgroundTransition manager;
    private void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<BackgroundTransition>();
        StartScore();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartScore()
    {
        StartCoroutine(goScore());
    }

    System.Collections.IEnumerator goScore()
    {
        if ((Seconds > 59) || (Seconds == 59))
        {
            Seconds = 0;
            Minutes++;
            manager.SwitchBackground();
        }
        Seconds++;
        Total++;
        Timer.text = Minutes + ":" + Seconds;
        yield return new WaitForSeconds(1f);
        if (!Stopclock)
        {
            StartCoroutine(goScore());
        }
    }
    public int GetScore()
    {
        return Total;
    }
}
