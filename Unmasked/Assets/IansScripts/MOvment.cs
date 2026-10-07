using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.UI.Image;

public class MOvment : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float JumpForce = 15f;
    [SerializeField] private KeyCode left = KeyCode.A;
    [SerializeField] private KeyCode right = KeyCode.D;
    [SerializeField] private KeyCode up = KeyCode.W;
    [SerializeField] private KeyCode down = KeyCode.S;
    [SerializeField] private float checkDistance;
    private bool isGrounded = false;
    private bool showDebugRay = true;


    public LayerMask Ground;
    private PolygonCollider2D capsuleCollider;

    private Rigidbody2D rb;

// In Start() or Awake():


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        capsuleCollider = GetComponent<PolygonCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }
    bool CheckGround()
    {
       
        // Start ray slightly above the bottom of the collider to avoid clipping issues
        Vector2 origin = transform.position;
        float rayLength = checkDistance;
        bool isGrounded = false;
        if (capsuleCollider != null)
        {
            origin = (Vector2)transform.position + Vector2.up * 0.05f; // small offset
            rayLength = capsuleCollider.bounds.extents.y + checkDistance;
        } 
        if (showDebugRay)
        {
            Color rayColor = isGrounded ? Color.green : Color.red;
            Debug.DrawRay(origin, Vector2.down * rayLength, rayColor);
        }

        // Perform raycast
        isGrounded = Physics2D.Raycast(origin, Vector2.down, rayLength, Ground);
        
        // Debug visualization

        return isGrounded;
    }
    void Update()
    { 
        
        if (Input.GetKey(left))
        {
            //Debug.Log("left");
            transform.Translate(Vector2.left* movementSpeed * Time.deltaTime);
         }

         if (Input.GetKey(right))
        {
      // Debug.Log("right");
           transform.Translate(Vector2.right * movementSpeed * Time.deltaTime);
        }
         
         if (Input.GetKey(up))
        {
            if(isGrounded)
            {
                rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
                isGrounded = false;
                // Debug.Log("Jump");
            }

                // Debug.Log("right");
                
        }/*
         if (Input.GetKey(down))
        {
            // Debug.Log("right");
            transform.Translate(Vector3.down * movementSpeed * Time.deltaTime);
        }
         */
        

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
         isGrounded = CheckGround();

    }

}


// Update is called once per frame


