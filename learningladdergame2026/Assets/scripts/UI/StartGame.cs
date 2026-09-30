using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartGame : MonoBehaviour
{
    [SerializeField] private Canvas menu;
    public AI Bot;
    public Camera camera1;   public Camera camera2;
    public void GoGame ()
    {
        Bot.enabled = false;
      camera1.depth= 0f;
        camera2.depth = -1f;
        menu.enabled = false;
        EventSystem.current.SetSelectedGameObject(null);
    }

}
