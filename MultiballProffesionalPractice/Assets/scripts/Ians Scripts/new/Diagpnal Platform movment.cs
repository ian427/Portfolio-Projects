using UnityEngine;

public class DiagpnalPlatformmovment : MonoBehaviour
{

    [SerializeField] private Vector2 lineStart;
    [SerializeField] private Vector2 lineEnd;
    public bool selected = false;

    void Update()
    {
 
        if (selected)
        {
            // Mouse to world pos
            Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos = new Vector2(mouse.x, mouse.y);

            // Direction of the diagonal (Vector2)
            Vector2 lineDir = lineEnd - lineStart;
            Vector2 lineDirNorm = lineDir.normalized;

            // Vector from start of line to mouse
            Vector2 startToMouse = mousePos - lineStart;

            // Project mouse onto the diagonal
            float distanceAlongLine = Vector2.Dot(startToMouse, lineDirNorm);

            // Clamp so the object stays between start and end
            distanceAlongLine = Mathf.Clamp(distanceAlongLine, 0f, lineDir.magnitude);

            // Final computed movement point
            Vector2 finalPos = lineStart + lineDirNorm * distanceAlongLine;

            // Move object
            transform.position = new Vector3(finalPos.x, finalPos.y, transform.position.z);
        }
    }
}
