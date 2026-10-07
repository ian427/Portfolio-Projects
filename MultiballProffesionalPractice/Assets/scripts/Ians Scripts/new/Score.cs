using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Timer;
    [SerializeField] private int Total;//score
    public bool Stopclock = false;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void AddScore(int toadd)
    {
        if (!Stopclock)
        {
            Total += toadd;
            Timer.text = Total.ToString(); ;
        }
    }
    public int GetScore()
    {
        return Total;
    }
    
}
