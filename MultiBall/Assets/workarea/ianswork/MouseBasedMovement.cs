using UnityEngine;

public class MouseBasedMovement : MonoBehaviour
{



    private Vector3 Move;
    [SerializeField] private float YLevel;
    [SerializeField] private float minX, maxX;
    // Start is called before the first frame update
    void Start()
    {

    }
    
    // Update is called once per frame
    void Update()
    {
        Move = Camera.main.ScreenToWorldPoint(Input.mousePosition);//converts pixel cords to mouse pos
        Move.z = 0f;
        Move.y = YLevel; if (Move.x < minX)// points to not go out of the level
        {
            var newPos = transform.position;
            Move.x = minX;
            
        }

        if (Move.x > maxX)
        {
            var newPos = transform.position;
            Move.x = maxX;
            
        }
        transform.position = Move;
    }
}

