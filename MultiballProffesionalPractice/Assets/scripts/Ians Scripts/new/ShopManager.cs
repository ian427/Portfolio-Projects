using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;


public class ShopManager : MonoBehaviour
{
    public List<GameObject> Pannels = new List<GameObject>();
    private List<PannelManager> PannelsScripts = new List<PannelManager>();
    public TexturesList tex;
    DataSerilizer Data;
    public int CurrentBallTextureID;//ball settings
     SecureSaverSerilizer Secure;
    public GameObject DisplayBall;
    //[ SerializeField] Sprite DisplayBallsprite;
    public int GCrystal = 0, RCrystal = 0, CCrystal = 0, GoldCrystal = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Debug.Log("Start");
        tex = GameObject.Find("DataHolder").GetComponent<TexturesList>();
        Data = GameObject.Find("DataHolder").GetComponent<DataSerilizer>();
        Secure = GameObject.Find("DataHolder").GetComponent<SecureSaverSerilizer>();
        //DisplayBallsprite = DisplayBall.GetComponent<Image>().sprite;
        foreach (GameObject pannel in Pannels)
        {
            PannelsScripts.Add(pannel.GetComponent<PannelManager>());
        }
        CurrentBallTextureID = Data.GetBallSettings();
        for (int i = 0; i < PannelsScripts.Count; i++)//loading all textures for this script
        {
            PannelsScripts[i].ID = i;
        }
        var (g, r, c, gl) = Secure.GetCurrency();
        GCrystal=g;
        RCrystal = r;
        CCrystal = c;
        GoldCrystal = gl;
       
    }
    private void Start()
    {
        PannelsScripts[CurrentBallTextureID].Clicked();
    }

    public (Sprite sprite, int id,bool lcked) GetPlannetData(int i)
    {
        
        Sprite sprite = tex.Textures[i].sprite;
        int ID = tex.Textures[i].data.TextureID;
        bool lk = tex.Textures[i].data.unlocked;

        return (sprite, ID,lk);
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void Unlock(int ID)
    {
        Secure.SetUnlocked(ID, true);
    }
    public void Equip(int ID)
    {
        CurrentBallTextureID = ID;
        DisplayBall.GetComponent<Image>().sprite = tex.Textures[ID].sprite;
       
    }
    public void SaveBallsettings()
    {
    
        if (tex.Textures[CurrentBallTextureID].data.unlocked)
        {
            Data.SetBallSettings(CurrentBallTextureID);
            Data.SaveBallSettings();

        }

       

    }
    public void SetPreviousFalse()
    {
        PannelsScripts[CurrentBallTextureID].Selected.enabled= false;
    }
    public void ChangeCurrency(int gCrystal, int rCrystal, int cCrystal, int goldCrystal)
    {
        GCrystal += gCrystal;
        RCrystal += rCrystal;
        CCrystal += cCrystal;
        GoldCrystal += rCrystal;
        Secure.SetCurrency(GCrystal, RCrystal, CCrystal, GoldCrystal);
            
    }
    public void Switchshop()
    {

    }
}
