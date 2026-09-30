using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UniversalMenuButton : MonoBehaviour
{
    [SerializeField] private Canvas menu ;
     private bool state = false;
    public void MenuSwitch()
    {
        state = !state;
        if (state)
        {
            menu.enabled = true;
        }
        else
        {
            menu.enabled = false;
        }
        EventSystem.current.SetSelectedGameObject(null);

    }

}
