using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntMoveObject : MonoBehaviour
{
    private GameControler controler;
    private int clicks = 0;
    [SerializeField] private bool CanPlace = false;
    [SerializeField] float ClickTolarence = 0.25f;
    private Vector3 Move;
    //private MonsterGrow monster;
    // Start is called before the first frame update
    void Start()
    {
        
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
        if (Input.GetMouseButtonDown(0))
        {
            clicks++;
            CanPlace = !CanPlace;
            if (clicks < 1)
            {
                clicks = 0;
                //ask to deleate
            }

        }
    }
}
