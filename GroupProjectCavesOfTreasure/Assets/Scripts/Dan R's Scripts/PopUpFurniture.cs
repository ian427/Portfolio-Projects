using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PopUpFurniture : MonoBehaviour
{
    public GameObject popUpMenu;
    public GameObject Currentitem;
    public DeleateThisObj button;
    //Double clicking variables
    public float firstClickTime;
    private float timeInbetweenClicking = 0.5f;
    public bool isTimeCheckAllowed = true;
    public int clickNumber = 0;

    //Sets the popUpMenu to be inactive upon the start
    void Start()
    {
        button = GameObject.Find("Confirm").GetComponent<DeleateThisObj>();
        popUpMenu.SetActive(false);
    }
   
   
    public IEnumerator DetectDoubleClick()
    {
        isTimeCheckAllowed = false;
        while(Time.time < firstClickTime + timeInbetweenClicking)
        {
            if(clickNumber == 2)
            {
               // Debug.Log("PopUp");
                button.ThisItem(Currentitem);
                popUpMenu.SetActive(true); 
                break;
            }
            yield return new WaitForEndOfFrame();
        }
        clickNumber = 0;
        isTimeCheckAllowed = true;
    }
}
