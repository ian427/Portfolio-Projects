using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjFallMove : MonoBehaviour
{
    public float fallSpeed = 0.01f;

    private void FixedUpdate()
    {
        gameObject.transform.position = new Vector3(transform.position.x, transform.position.y - fallSpeed);
    }
}
