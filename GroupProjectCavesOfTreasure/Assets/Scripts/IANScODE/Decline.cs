using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Decline : MonoBehaviour
{
    PopUpFurniture PopUpMenu;

    // Start is called before the first frame update
    void Start()
    {
        PopUpMenu = GameObject.Find("EventSystem").GetComponent<PopUpFurniture>();
    }
    public void OnButtonPress()
    {
        Debug.Log("down");
        PopUpMenu.popUpMenu.SetActive(false);

    }
}
