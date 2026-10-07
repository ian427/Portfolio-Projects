using System;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using static SecureSaverSerilizer;



public class TexturesList : MonoBehaviour
{
    public Sprite[] planets;
    public SecureSaverSerilizer Secure;
    [Serializable]
    public class TextureCell
    {
        public Sprite sprite;            // Or Texture2D if you prefer
        public SecureSaverSerilizer.Unlockdata data = new SecureSaverSerilizer.Unlockdata();
    }
     public List<TextureCell> Textures = new List<TextureCell>();
    private int NumberofTextures = 0; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

       

        for (int i = 0 ; i < Textures.Count;i++)
        {
            SecureSaverSerilizer.Unlockdata temp = Secure.GetUnlocked(i);
        }

    }
    private void Awake()
    {
        Secure = GetComponent<SecureSaverSerilizer>();
        //////////////////////////////////////////// suspend for fixed id placment
        planets = Resources.LoadAll<Sprite>("GeneratedPlanets");
        
        for (int i = 0; i < planets.Length; i++)
        {
            TextureCell Temp = new TextureCell();
            Temp.sprite = planets[i];
            Textures.Add(Temp);
        }
        
        ////////////////////////////////////////
        NumberofTextures = Textures.Count;
        Secure.LoadAllUnlocks(NumberofTextures);//loading textures in Secure Storage Not this script
        for (int i = 0; i < Textures.Count; i++)//loading all textures for this script
        {
            Textures[i].data.TextureID = i;
            SecureSaverSerilizer.Unlockdata Temp = Secure.GetUnlocked(i);
            if (Temp.TextureID == i)//data is good
            { Textures[i].data = Temp; }
            else { Debug.Log("BadTexture" + i); }
            
        }
    }
    public void UnlockNewTexture(int tounlock)
    {
        Secure.SetUnlocked(tounlock, true);
        Textures[tounlock].data.unlocked = true;
    }
    public List<TextureCell> GetLockedTextures()
    {
        List<TextureCell> lockedtex = new List<TextureCell>(Textures);
        lockedtex.RemoveAll(cell => cell.data.unlocked == true);
        return lockedtex;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
