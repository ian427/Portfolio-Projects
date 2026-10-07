using UnityEngine;

public class tiltPlatform : MonoBehaviour
{
    public float minDelay = 1f;
    public float maxDelay = 3f;

    public float flickSpeed = 800f;
    public float returnSpeed = 200f;

    private float targetRotation = 0f;
    private bool CanFlick = true;

    void OnCollisionEnter2D(Collision2D col)
    {
        targetRotation = Random.Range(40f, 100f);
        StartCoroutine(Flick());

    }
    void RotateUpdate()
    {
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            Quaternion.Euler(0, 0, targetRotation),
            (targetRotation != 0 ? flickSpeed : returnSpeed) * Time.deltaTime
        );//moves back to zero position


    }


    System.Collections.IEnumerator Flick()
    {
        CanFlick = false;
        transform.rotation = Quaternion.RotateTowards(
           transform.rotation,
           Quaternion.Euler(0, 0, targetRotation),
           (targetRotation != 0 ? flickSpeed : returnSpeed) * Time.deltaTime

        );
        RotateUpdate();
        yield return new WaitForSeconds(0.1f);
        RotateUpdate();
        CanFlick = true;
    }
}

