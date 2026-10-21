using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    public Transform smallOrbitP;
    public Vector3 desiredPos;
    public float radius = 2.5f;
    public float radiusSpeed = 0.5f;
    public float rotSpeed = 150.0f;

    public bool isPressed = false;

    // Update is called once per frame
    void Update()
    {
        //counter clockwise rotation
        transform.RotateAround(smallOrbitP.localPosition, Vector3.forward, Time.deltaTime * rotSpeed);

        if (isPressed)
        {
            desiredPos = (transform.position - smallOrbitP.position).normalized * radius + smallOrbitP.position;
            transform.position = Vector3.MoveTowards(transform.position, desiredPos, Time.deltaTime * radiusSpeed);
        }
        else
        {
            desiredPos = (transform.position - smallOrbitP.position).normalized / radius + smallOrbitP.position;
            transform.position = Vector3.MoveTowards(transform.position, desiredPos, Time.deltaTime * radiusSpeed);
        }
    }

    public void OnMove(InputAction.CallbackContext touch)
    {
        //hold thing down, increase orbit
        if (touch.action.phase == InputActionPhase.Started)
        {
            isPressed = true;
        }
        else if (touch.action.phase == InputActionPhase.Canceled)
        {
            isPressed = false;
        }
    }
}
