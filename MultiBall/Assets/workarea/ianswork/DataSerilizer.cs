using UnityEngine;

public class DataSerilizer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int HighTime = 0;
    public int Brightness = 255;
    public float Volume = 1;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if(!PlayerPrefs.HasKey("HighScore")) { PlayerPrefs.SetInt("HighScore", HighTime);}
        else {HighTime = PlayerPrefs.GetInt("HighScore");}
        if (!PlayerPrefs.HasKey("Brightness")) { PlayerPrefs.SetInt("Brightness", Brightness);}
        else {Brightness = PlayerPrefs.GetInt("Brightness");}
        if (!PlayerPrefs.HasKey("Volume")) { PlayerPrefs.SetFloat("Volume", Volume);}
        else {Volume = PlayerPrefs.GetFloat("Volume");}

    }
    void Start()
    {
        
    }
    public void LoadHighTime()//loading from prefs
    {
        HighTime = PlayerPrefs.GetInt("HighScore");
    }
    public void SaveHighestime ()//saving to prefs
    {
        PlayerPrefs.SetInt("HighScore", HighTime);
    }

    public void SetHighTime(int time)//in game
    {
        
        HighTime = time;
        SaveHighestime();
        
    }
    public int GetHighTime()
    {
       
        return HighTime;
    }
    
    public void SetSettings()
    {
        PlayerPrefs.SetInt("Brightness", Brightness);
        PlayerPrefs.SetFloat("Volume", Volume);
    }
    public void GetSettings()
    {
        Brightness = PlayerPrefs.GetInt("Brightness");
        Volume = PlayerPrefs.GetFloat("Volume");
    }
}