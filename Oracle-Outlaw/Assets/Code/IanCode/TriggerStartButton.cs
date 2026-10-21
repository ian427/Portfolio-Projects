using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class TriggerStartButton : MonoBehaviour
{
    private bool ButtonPressed = false;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {     
        if (ButtonPressed == true)
        {
            anim.SetBool("Boom", true);
            Debug.Log("boom");
        }
    }

    public bool getButtonPressed()
    {
        return ButtonPressed;
    }

    public void setButtonPressed(bool pressedstat)
    {
        ButtonPressed = pressedstat;
    }
}
