using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MandrillAnimations : MonoBehaviour
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
    public void EndBlock()
    {
        Debug.Log("MBlocked");
    }
    public void EndPunch()
    {
        Debug.Log("MPunched");
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && !isDodging)
        {
            anim.Play("Block");
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            anim.Play("Idle");
        }
        //Dodge animation
        if (Input.GetKeyDown(KeyCode.DownArrow) && !isDodging)
        {
            anim.Play("Dodge");
            //isDodging = true;
        }
        //Attack animation
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !isDodging)
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
        if (Input.GetKeyDown(KeyCode.RightArrow) && !isDodging)
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
    }
}