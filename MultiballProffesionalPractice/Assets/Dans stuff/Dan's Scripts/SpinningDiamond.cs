using UnityEngine;

public class SpinningDiamond : MonoBehaviour
{
    private float turnSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        turnSpeed = 5 * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, turnSpeed);
    }
}
