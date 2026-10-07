using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AttractToCenter2D : MonoBehaviour
{
    public Transform target;     // Drag your GameObject here in Inspector
    public float force = 5f;     // How strong the pull is

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (target == null) return;

        // Direction from ball ? center
        Vector2 direction = target.position - transform.position;

        // Normalized direction × force = constant pull strength
        rb.AddForce(direction.normalized * force, ForceMode2D.Force);
    }
}
