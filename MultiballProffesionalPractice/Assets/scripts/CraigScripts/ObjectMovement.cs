using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectMovement : MonoBehaviour
{
    public float speed = 5f;
    public float minX, maxX;
    Vector2 _direction;
    public Rigidbody2D rb;

    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log(context.ReadValue<Vector2>());
        _direction = context.ReadValue<Vector2>();//direction for the varable
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(_direction * (speed * Time.deltaTime));// movement

        if (transform.position.x < minX)// points to not go out of the level
        {
            var newPos = transform.position;
            newPos.x = minX;
            transform.position = newPos;
        }

        if (transform.position.x > maxX)
        {
            var newPos = transform.position;
            newPos.x = maxX;
            transform.position = newPos;
        }

        /*if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0f, 0f);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position -= new Vector3(speed * Time.deltaTime, 0f, 0f);
        }*/
    }
}
