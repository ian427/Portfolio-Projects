using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public DataSerilizer Data;
    public Score score;
    //[SerializeField] private int alphavalue ;
    [SerializeField] private TextMeshProUGUI HighScoretxt, MainHighScoretxt;
    [SerializeField] private TextMeshProUGUI CurrentScoretxt;
    //public float FadeDelay = 1f;
    //public float AlphaValue = 0;
    [SerializeField] public Canvas Pannel;
    //private Image image;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private Spawnpointhandler spawners;
    void Start()
    {
        score = GetComponent<Score>();
        Data = GameObject.Find("DataHolder").GetComponent<DataSerilizer>();
        //Pannel.enabled = true;
        int Temp;
        Temp = Data.GetHighTime();
        int Minutes = 0, seconds = 0;
        while (Temp > 60)
        {
            Temp -= 60;
            Minutes++;

        }
        seconds = Temp;
        MainHighScoretxt.text = Minutes + ":" + seconds;
    }
        


    public void GameOver()
    {
        Pannel.enabled = true;
        score.Stopclock = true;
        spawners.CanSpawn = false;
        int Highscore = Data.GetHighTime();
        int newscore = score.GetScore();
        if(newscore > Highscore)
        {
            Data.HighTime = newscore;
            Data.SaveHighestime();
            //show new score
           
            int Minutes = 0,seconds = 0;
            while (newscore > 60)
            {
                newscore -= 60;
                Minutes++;

            }
            seconds = newscore;
            HighScoretxt.text = Minutes + ":" + seconds;
            CurrentScoretxt.text = Minutes + ":" + seconds;
            
        }
        else
        {
            int SMinutes = 0, SSeconds = 0, HMinutes = 0, HSeconds = 0;
            //show old score 
            while (newscore > 60)
            {
                newscore -= 60;
                SMinutes++;

            }
            SSeconds = newscore;
            while (Highscore > 60)
            {
                Highscore -= 60;
                HMinutes++;

            }
            HSeconds = Highscore;
            HighScoretxt.text = HMinutes + ":" + HSeconds;
            CurrentScoretxt.text = SMinutes + ":" + SSeconds;
        }
       
    }
   

}
