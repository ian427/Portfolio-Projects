using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public SecureSaverSerilizer Data;
    public Score score;
    //[SerializeField] private int alphavalue ;
    [SerializeField] private TextMeshProUGUI HighScoretxt ;
    [SerializeField] private TextMeshProUGUI CurrentScoretxt;
    //public float FadeDelay = 1f;
    //public float AlphaValue = 0;
    [SerializeField] public Canvas Pannel;
    //private Image image;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
   // private Spawnpointhandler spawners;
    public Spawnballs ballspawner;
    [SerializeField] private celebrationfx Celebration;
    public Scores ScoreData;
    [SerializeField] private GameObject Fade;
    [SerializeField] private float startdelay = 1;

    void Start()
    {
        score = GetComponent<Score>();
        Data = GameObject.Find("DataHolder").GetComponent<SecureSaverSerilizer>();
        
        //Pannel.enabled = true;
       
       
       // MainHighScoretxt.text = Temp.ToString();
    }
public void GameOver()
    {
        score.Stopclock = true;
        ballspawner.Freezeballs();
        StartCoroutine(PlayAnimationCoroutine());
      

    }

    System.Collections.IEnumerator PlayAnimationCoroutine()
    {
        yield return new WaitForSeconds(startdelay);
        Fade.SetActive(true);
        yield return new WaitForSeconds(1);
        Setendpannel();
    }
    
    private void Setendpannel ()
    {
        Pannel.enabled = true;
       
        // spawners.CanSpawn = false;
        
        int Highscore = Data.GetHighTime();
        int newscore = score.GetScore();

        if (newscore > Highscore)
        {
            Celebration.PlayCelebration();
            Data.HighTime = newscore;
            Data.SaveHighestime();
            //show new score


            HighScoretxt.text = newscore.ToString();
            CurrentScoretxt.text = newscore.ToString();

        }
        else
        {

            HighScoretxt.text = Highscore.ToString();
            CurrentScoretxt.text = newscore.ToString();
        }
        ScoreData.HighScore = Highscore;
        ScoreData.Curentscore = newscore;

    }
   

}
