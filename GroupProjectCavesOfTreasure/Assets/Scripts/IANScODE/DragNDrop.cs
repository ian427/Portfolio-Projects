using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class DragNDrop : MonoBehaviour
{
    private GameControler controler;
    private GameObject TempItem;
    private Renderer renderer;
    public GameObject NonFunctionalDuplicate;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        controler = GameObject.Find("Controller").GetComponent<GameControler>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void OnMouseOver()
    { 
        renderer.material.color = Color.red;
        //renderer.material.color = Color.red;
        if (Input.GetMouseButtonDown(0))
        {
            if (controler.CheckFurniture(this.gameObject.GetComponent<SpriteRenderer>().sprite))//checking if we have more than zero of this item
            {
                controler.RemoveItem(this.gameObject.GetComponent<SpriteRenderer>().sprite);
                TempItem = GameObject.Instantiate(NonFunctionalDuplicate);
                TempItem.transform.position = this.transform.position;//spawns bullet at position 
                controler.CurrentplacedFurnitureL.Add(TempItem);
               // controler.furnitureData.CurrentlyPlacedFurniture.Add(TempItem);

            }
        }
    }
    private void OnMouseExit()
    {
        renderer.material.color = Color.white;
    }
}
