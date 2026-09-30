using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    private float movementSpeed = 3;
    public DisplayWindow Display;
    public bool FireControl = true;
    public AudioSource cannonFire;

    void Start()
    {
        Display = GameObject.Find("Window").GetComponent<DisplayWindow>();

    }

    void Update()
    {

        if (Input.GetKey(KeyCode.A))
        {
            //Debug.Log("left");
            transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
        }

        else if (Input.GetKey(KeyCode.D))
        {
           // Debug.Log("right");
            transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
        }
        else if(Input.GetKey(KeyCode.Space)&&(FireControl == true))
        {
            cannonFire.Play();
            Display.CanSpawn = true;
            GameObject Shot = Display.Tempbullet  ;
            GameObject TempShot = Instantiate(Shot); 
            TempShot.transform.position = this.transform.position;
            TempShot.GetComponent<ShotMove>().enabled = true;
        }
        

    }
   
}

