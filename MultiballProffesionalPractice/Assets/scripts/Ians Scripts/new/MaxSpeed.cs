using UnityEngine;

public class MaxSpeed : MonoBehaviour
{ 
    [SerializeField] private float maxSpeed = 1f;
    Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     rb = GetComponent<Rigidbody2D>();   
    }

 
   

    void FixedUpdate()
    {
        // Option A: Using ClampMagnitude (clean & readable)
        rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, maxSpeed);

        // Option B: Manual (same result)
        // if (rb.velocity.sqrMagnitude > maxSpeed * maxSpeed)
        // {
        //     rb.velocity = rb.velocity.normalized * maxSpeed;
        // }
    }

}
