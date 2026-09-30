using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeleateThisObj : MonoBehaviour
{
    PopUpFurniture PopUpMenu;
    public GameObject ToDestroy;
    private FurnitureControler furniturecontroler;

    // Start is called before the first frame update
    void Start()
    {
        PopUpMenu = GameObject.Find("EventSystem").GetComponent<PopUpFurniture>();
    }
    public void ThisItem(GameObject item)
    {
        ToDestroy = item;
    }
    public void OnButtonPress()
    {
        Debug.Log("yes");
        furniturecontroler = ToDestroy.GetComponent<FurnitureControler>();
        furniturecontroler.DeleatItem();
        PopUpMenu.popUpMenu.SetActive(false);
       
    }

}
