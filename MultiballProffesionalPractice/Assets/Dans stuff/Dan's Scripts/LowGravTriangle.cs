using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class LowGravTriangle : MonoBehaviour
{
    [SerializeField] private GameObject ballObject;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            ballObject = collision.gameObject;
            ballObject.GetComponent<AttractToCenter2D>().force = 1;
            StartCoroutine(resetGravity());
        }
    }

    private IEnumerator resetGravity()
    {
        yield return new WaitForSeconds(3);
        ballObject.GetComponent<AttractToCenter2D>().force = 5;
    }
}
