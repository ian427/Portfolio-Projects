using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ONGAMEQUIT : MonoBehaviour
{
    FurnitureData data;
    public string SGoTo ;
    // Start is called before the first frame update
    void OnApplicationQuit()
    {
        data = (FurnitureData)Resources.Load("GameData");
        data.SaveGameData();
        
    }
    public void Tomenu()
    {
        data = (FurnitureData)Resources.Load("GameData");
        data.SaveGameData();
        SceneManager.LoadScene(SGoTo);
    }
}
