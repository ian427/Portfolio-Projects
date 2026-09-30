using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public class UnitController : MonoBehaviour
{
    public SimulationSettings settings;
    public Gridtest Grid;
    private Vector3 Destination;
    private Vector3 NextMoveLocation;
    private Vector3 currentPos;
    private int MovementDistance;
    private float Speed = 1;
    private Rigidbody rb;
    private float elapsedTime = 0f;
    private bool isSliding = false;
    public bool SlidingInterupted = false;
    private float slideDuration;
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        Grid = GameObject.Find("grid generator").GetComponent<Gridtest>();
    }
    void Update()
    {
        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
           TestMovment();
        }
    }
   
    void FixedUpdate()
    {
        if (!isSliding) return;

        Vector3 currentPos = rb.position;
        Vector3 direction = Destination - currentPos;
        float distanceToTarget = direction.magnitude;

        if (distanceToTarget <= 0.01f)
        {
            isSliding = false;
            return;
        }

        float step = Speed * Time.fixedDeltaTime;
        if (step > distanceToTarget) step = distanceToTarget;

        Vector3 nextPosition = currentPos + direction.normalized * step;
        rb.MovePosition(nextPosition);

        if (Grid != null)
            Grid.UpdateHeatmap(nextPosition, gameObject.tag);
    }
    public void SimUpdate()
    {
        switch (settings.GameState)
        {
            case 0: // Calculate next movement spot
                currentPos = this.gameObject.transform.position;
                Vector3 direction = Destination - currentPos;
                float distanceToTarget = direction.magnitude;

                if (distanceToTarget <= 0.01f)
                {
                    // Already at or very close to the destination
                    NextMoveLocation = currentPos;
                }
                else if (distanceToTarget <= MovementDistance)
                {
                    // Move exactly to the target because it's closer than movement distance
                    NextMoveLocation = Destination;
                }
                else
                {
                    // Move by MovementDistance toward the target
                    Vector3 unitDirection = direction / distanceToTarget;
                    NextMoveLocation = currentPos + unitDirection * MovementDistance;
                }
                break;

            case 1: // Execute movement
                currentPos = this.gameObject.transform.position;
                float moveSpeed = settings.SimSpeed * Speed;
                BeginSlide(currentPos, moveSpeed);
                break;
        }
    }
    public void SetDestination(int X, int Z)//for commander
    {
        Destination = new Vector3 (X,0,Z);
        return;
    }
    private void BeginSlide(Vector3 Pos, float Movespeed)
    {

        
        slideDuration = Mathf.Max(Movespeed, 0.01f); // prevent divide by zero
        elapsedTime = 0f;
        isSliding = true;
    }
    void OnCollisionEnter(Collision collision)
    { 
        if( (isSliding) && ((collision.gameObject.tag == "Player1") || (collision.gameObject.tag == "Player2")))
        {
            isSliding = false; // Stop movement logic
            SlidingInterupted = true;                // Do something else, like notify the game controller
        }
    }
    public bool SucessfulMove()//for commander
    { return SlidingInterupted; }
    private void TestMovment()
    {
        SetDestination(10, 10);
        transform.position = new Vector3(1,1,1);
        float moveSpeed = settings.SimSpeed * Speed;
        BeginSlide(currentPos, moveSpeed);

    }


}