using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimator : MonoBehaviour
{
    [SerializeField] private float openingSpeed = 1.0f;
    [SerializeField] private float openingAmount = 4.5f; 

    public GameObject doorLeft;
    public GameObject doorRight;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if ( doorLeft.transform.position.x > -openingAmount && doorRight.transform.position.x < openingAmount )
        {
            doorLeft.transform.position -= new Vector3( openingSpeed, 0f, 0f ) * Time.deltaTime;
            doorRight.transform.position += new Vector3( openingSpeed, 0f, 0f ) * Time.deltaTime;
        }
    }
}
