using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class gotomenu : MonoBehaviour
{
    public Camera camera1; public Camera camera2;
    // Start is called before the first frame update
    [SerializeField] private Canvas menu;
    public void Gotomenu()
    {
        camera1.depth = -1f;
        camera2.depth = 0f;
        menu.enabled = true;
        EventSystem.current.SetSelectedGameObject(null);
    }
}
