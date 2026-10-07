using System.Collections;
using UnityEngine;

public class PinningAway : MonoBehaviour
{
    [SerializeField] private GameObject ballObject;
    [SerializeField] private Rigidbody2D rb;

    private float pingSpeed;
    private float normalSpeed;

    private Vector2 direction;
    
    void Start()
    {
        pingSpeed = 30f;
        normalSpeed = 1f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            ballObject = collision.gameObject;
            rb = ballObject.GetComponent<Rigidbody2D>();

            if(rb != null)
            {
                direction = (collision.transform.position - transform.position).normalized;
            }

            StartCoroutine(ResetSpeed(rb, direction));
        }
    }

    private IEnumerator ResetSpeed(Rigidbody2D rb, Vector2 direction)
    {
        rb.linearVelocity = direction * pingSpeed;
        yield return new WaitForSeconds(0.2f);
        rb.linearVelocity = rb.linearVelocity.normalized * normalSpeed;
    }
}
