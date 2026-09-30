using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class enableAI : MonoBehaviour
{
    [SerializeField] private Canvas menu;
    public AI Bot;
    public Camera camera1;    public Camera camera2;
    public void GoAI()
    {
        menu.enabled = false;
        Bot.enabled = true;
        camera1.depth = 0f;
        camera2.depth = -1f;
            EventSystem.current.SetSelectedGameObject(null);
    }

}
