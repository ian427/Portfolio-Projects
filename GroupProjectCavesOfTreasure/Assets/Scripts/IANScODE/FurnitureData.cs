using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using Unity.Mathematics;
//using UnityEditor.Search;
//using UnityEditor.Tilemaps;
using UnityEngine;
[CreateAssetMenu(fileName ="GameData",menuName ="newfurniture")]
public class FurnitureData : ScriptableObject
{
    public List<Sprite> Furniture;//needs to be shop variant
    public int[] AmountOfFurniture;//furnitur index
    public float TotalItiems = 1;

    public int Gold = 0;
    public int Diamond = 0;
    public int Gem = 0;
    public TimeSpan LastDate;
    public string Path;
    public List<Sprite> CurrentlyDisplayedT1;
    public List<Sprite> CurrentlyDisplayedT2;
    public Sprite currentlydisplayedT3;
    public TimeSpan LastHuntTime;
    public long LastRewardDate;
    public int CurrentNumberOfHunts;
    public List<GameObject> CurrentlyPlacedFurniture;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadGameData()
    {
        string s;
        // add execute only in play mode vs build mode
        //Path = Application.persistentDataPath;
       // string s = Path + "\\SaveGame.txt";
        //IF EDITOR 
       Path = Application.dataPath;
       s = (Path +"\\Scripts\\SaveGame.txt");
       

        Debug.Log(Path);
       
        string data = File.ReadAllText(s);
        JsonUtility.FromJsonOverwrite(data, this);
    }
    public void SaveGameData()
    {
        Debug.Log(Path);
        Path = Application.dataPath;
        var fd = (FurnitureData)Resources.Load("GameData");
        //saves data to player prefs
        string s = JsonUtility.ToJson(this);
        File.WriteAllText((Path +"\\Scripts\\SaveGame.txt"), s);
    }

}

