using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    private Animator anim;

    private int choice = 0;

    private bool isDodging = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Block animation
        if (Input.GetKeyDown(KeyCode.W) && !isDodging)
        {
            anim.Play("Block");
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            anim.Play("Idle");
        }
        
        //Dodge animation
        if (Input.GetKeyDown(KeyCode.S) && !isDodging)
        {
            anim.Play("Dodge");
            //isDodging = true;
        }
        //if (Input.GetKeyUp(KeyCode.S))
        //{
        //    anim.Play("Idle");
        //}
        
        //Attack animation
        if (Input.GetKeyDown(KeyCode.A) && !isDodging)
        {
            choice = Random.Range(1, 3);
            if (choice == 1)
            {
                anim.Play("Attack 1");
            }
            if (choice == 2)
            {
                anim.Play("Attack 2");
            }
        }
        //if (Input.GetKeyUp(KeyCode.A))
        //{
        //    anim.Play("Idle");
        //}
        
        if (Input.GetKeyDown(KeyCode.D) && !isDodging)
        {
            choice = Random.Range(1, 3);
            if (choice == 1)
            {
                anim.Play("Attack 1");
            }
            if (choice == 2)
            {
                anim.Play("Attack 2");
            }
        }
        //if (Input.GetKeyUp(KeyCode.D))
        //{
        //    anim.Play("Idle");
        //}
    }
    //public void unDodge()
    //{
    //    isDodging = false; //Add an event when the dodge animation finishes (frame 20) using the animation tab and call this function
    //}
}
