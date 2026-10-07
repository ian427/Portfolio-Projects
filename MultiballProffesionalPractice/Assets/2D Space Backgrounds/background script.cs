using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class InfiniteScrollingBackground2D : MonoBehaviour
{
    public float scrollSpeed = 0.5f;
    public float overlapFix = 0.01f;

    private float width;
    private static float rightMostX;

    void Start()
    {
        width = GetComponent<SpriteRenderer>().bounds.size.x;

        // Initialize the right-most position once
        if (rightMostX == 0f)
        {
            rightMostX = transform.position.x;
        }
        else
        {
            rightMostX = Mathf.Max(rightMostX, transform.position.x);
        }
    }

    void LateUpdate()
    {
        // Move left smoothly
        transform.position += Vector3.left * scrollSpeed * Time.deltaTime;

        // If fully off screen to the left, move to the right end
        if (transform.position.x < Camera.main.transform.position.x - width)
        {
            float newX = rightMostX + width - overlapFix;
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
            rightMostX = newX;
        }
    }
}
