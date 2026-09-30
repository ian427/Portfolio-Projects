using UnityEngine;

using System.Collections;
public class Bounce : MonoBehaviour
{    public Rigidbody2D rb;
    public float force = -40f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }
   
       
      void OnCollisionEnter2D(Collision2D col)
      {

        Vector3 movement = new Vector3(0, force, 0);
        rb.AddForce(movement * force);
      }
        

      
       

        // Update is called once per frame
        void Update()
    {
        
    }
}
