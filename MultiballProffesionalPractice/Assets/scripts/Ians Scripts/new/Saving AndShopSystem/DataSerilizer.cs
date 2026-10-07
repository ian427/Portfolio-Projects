using UnityEngine;

public class DataSerilizer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public int Brightness = 255;
    public float Volume = 1;
    public int CurrentTextureID = 0;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    
        if (!PlayerPrefs.HasKey("ID")) { PlayerPrefs.SetInt("ID", CurrentTextureID); }
        else { CurrentTextureID = PlayerPrefs.GetInt("ID"); }

        if (!PlayerPrefs.HasKey("Brightness")) { PlayerPrefs.SetInt("Brightness", Brightness);}
        else {Brightness = PlayerPrefs.GetInt("Brightness");}
        if (!PlayerPrefs.HasKey("Volume")) { PlayerPrefs.SetFloat("Volume", Volume);}
        else {Volume = PlayerPrefs.GetFloat("Volume");}

    }
    void Start()
    {
        
    }
    public void LoadBallSettings()//loading from prefs
    {
  
        CurrentTextureID = PlayerPrefs.GetInt("ID");
    }
    public void SaveBallSettings()//saving to prefs
    {

        PlayerPrefs.SetInt("ID", CurrentTextureID);
    }

    public void SetBallSettings(int ID)//in game
    {

      
        CurrentTextureID = ID;
        SaveBallSettings();
        
    }
    public int GetBallSettings()
    {

        return ( CurrentTextureID);
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