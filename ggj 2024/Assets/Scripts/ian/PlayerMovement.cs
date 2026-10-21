using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float MovementSpeed = 0.5f;

    [SerializeField]
    private float FastSpeed = 3f;

    private float Speed;

    [SerializeField]
    private float JumpHeight = 5;// forward Movement Speed
    private Rigidbody2D rb;
    
    public bool Grounded;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Speed = MovementSpeed;
       
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3) 
        {
         Grounded = true;
        }
    
    }
        // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.Space))
        {
           // Debug.Log("space");
            if (Grounded == true )
            {

                rb.AddForce(new Vector2(0.0f, JumpHeight), ForceMode2D.Impulse);
                Grounded = false; 
              //  Debug.Log("jumped");
            }
                
        }

        if(Input.GetKey(KeyCode.LeftShift))
        {
            Speed = FastSpeed;
        }

        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            Speed = MovementSpeed;
        }


        gameObject.transform.position = new Vector3(transform.position.x + Speed, transform.position.y);


    }
}
