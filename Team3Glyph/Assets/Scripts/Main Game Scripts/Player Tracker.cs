using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerTracker : MonoBehaviour
{
    private Vector3 Move;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move = Camera.main.ScreenToWorldPoint(Input.mousePosition);//converts pixel cords to mouse pos
        Move .z = 0f;
        transform.position = Move;
    }
}


