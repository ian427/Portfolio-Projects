using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureControler : MonoBehaviour
{
    private GameControler controler;
    private float clickstime = 0;
    [SerializeField] public bool CanPlace = true;
    [SerializeField] float ClickTolarence = 0.25f;
    private Vector3 Move;
    private MonsterGrow monster;
    private PopUpFurniture popup;
    public float X, Y;

    public ParticleSystem placeItemVFX;

    // Start is called before the first frame update
    void Start()
    {
        controler = GameObject.Find("Controller").GetComponent<GameControler>();
        monster = GameObject.Find("Player").GetComponent<MonsterGrow>();
        popup = GameObject.Find("EventSystem").GetComponent<PopUpFurniture>();
        //CanPlace = true ;
        
        controler.TotalItems++;
        monster.UpdateMonsterSize();
        X = this.transform.position.x;
        Y = this.transform.position.y;
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
        else
        { 
            X = this.transform.position.x; 
            Y = this.transform.position.y;

        }

    }
    private void OnMouseOver()
    {
        //Debug.Log("Over");
        if (Input.GetMouseButtonDown(0))
        {
            CanPlace = !CanPlace;
           
        }
        if (Input.GetMouseButtonUp(0))
        {
            popup.clickNumber += 1;
            placeItemVFX.Play();
        }
        if (popup.clickNumber == 1 && popup.isTimeCheckAllowed)
        {
            popup.firstClickTime = Time.time;
            popup.Currentitem = this.gameObject;
            StartCoroutine(popup.DetectDoubleClick());
        }
    }
    public void DeleatItem()//removes from gameplay area
    {
       
        controler.TotalItems--;
        controler.AddItem(this.gameObject.GetComponent<SpriteRenderer>().sprite);
        controler.CurrentplacedFurnitureL.Remove(this.gameObject);
        monster.UpdateMonsterSize();
        Destroy(this.gameObject);
    }
    private void OnMouseExit()
    {
        popup.clickNumber = 0;
    }
    public void SetPosition( )
    {
        this.gameObject.transform.position = new Vector3(X,Y,0);

    }
    public void SavePosition()
    {
        X = this.transform.position.x;
        Y = this.transform.position.y;
    }

}

        