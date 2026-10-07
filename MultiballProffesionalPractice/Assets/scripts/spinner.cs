using UnityEngine;

public class FerrisWheelSpinner : MonoBehaviour
{
    public float minSpin = -200f;
    public float maxSpin = 200f;

    private float spinSpeed;

    void Start()
    {
        spinSpeed = Random.Range(minSpin, maxSpin);
    }

    void Update()
    {
        transform.Rotate(0, 0, spinSpeed * Time.deltaTime);
    }
}
