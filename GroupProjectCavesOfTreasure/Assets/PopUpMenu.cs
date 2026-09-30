using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpMenu : MonoBehaviour
{
    public GameObject inventory;
    public static bool isInventoryOpen;

    void Start()
    {
        inventory.SetActive(false);
    }

    public void OpenInventory()
    {
        inventory.SetActive(true);
        isInventoryOpen = true;
    }

    public void CloseInventory()
    {
        inventory.SetActive(false);
        isInventoryOpen = false;
    }
}
