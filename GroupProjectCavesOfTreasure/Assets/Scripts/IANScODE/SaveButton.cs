using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveButton : MonoBehaviour
{
    FurnitureData data;
    private void Start()
    {
      data = (FurnitureData)Resources.Load("GameData");
    }
    public void OnButtonPress()
    {
        data.SaveGameData();
        Debug.Log("Saved");
    }
}
