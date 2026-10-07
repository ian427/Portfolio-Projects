using UnityEngine;

public class PaddelBounceDirectionControler : MonoBehaviour
{
    [SerializeField] private float Force = 5f;
    [SerializeField] private float RightBound = 85f;
    [SerializeField] private float LeftBound = -85f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();
        float RNGOfset = Random.Range(LeftBound, RightBound);

        col.transform.Rotate(0f, 0f, RNGOfset, Space.Self);
        Debug.Log(RNGOfset);
        Vector2 direction = col.transform.up;
        rb.AddForce(direction * Force, ForceMode2D.Impulse);
    }

   
}
