using UnityEngine;

public class PhoneMovement : MonoBehaviour
{
    private Vector3 Move;
    [SerializeField] private float YLevel;
    [SerializeField] private float minX, maxX;
   
    void Update()
    {
        Touch t = Input.GetTouch(0);
        Move = Camera.main.ScreenToWorldPoint(new Vector3(t.position.x, t.position.y, 0f));
        
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
