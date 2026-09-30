using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnviromentDNG : MonoBehaviour
{
  
   
    [SerializeField] private bool CanPlace = true;
    [SerializeField] private bool CanDestroy = false;

    private Vector3 Move;
   

    // Start is called before the first frame update
    void Start()
    {
        CanPlace = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanPlace)
        {
            Move = Camera.main.ScreenToWorldPoint(Input.mousePosition);//converts pixel cords to mouse pos
            Move.z = 0f;
            transform.position = Move;
            //goto mouse
        }

    }
    private void OnMouseOver()
    {
       // Debug.Log("Over");
        if (Input.GetMouseButtonDown(0))
        {
          CanPlace =!CanPlace;
            if ((CanPlace==false)&&(CanDestroy)) {Destroy(this.gameObject); } 
        }
    }
 
    private void OnTriggerExit2D(Collider2D collision)
    {
        CanDestroy = true;
    }
 
}
