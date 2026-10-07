using UnityEngine;

public class CircularPlayerControler : MonoBehaviour
{
    [SerializeField] private float RotationSpeed = 5f;
    [SerializeField] private KeyCode One = KeyCode.A;
    [SerializeField] private KeyCode Two = KeyCode.D;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(One))
        {
            transform.Rotate(0, 0, -RotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(Two))
        {
            transform.Rotate(0, 0, RotationSpeed * Time.deltaTime);
        }

    }
}
