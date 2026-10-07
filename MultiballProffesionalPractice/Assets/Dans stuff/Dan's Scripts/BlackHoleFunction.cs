using Unity.VisualScripting;
using UnityEngine;

public class BlackHoleFunction : MonoBehaviour
{
    [SerializeField] private GameObject[] points;
    private float randomNumber;

    private void Start()
    {
        points[0] = GameObject.Find("Point (1)");
        points[1] = GameObject.Find("Point (2)");
        points[2] = GameObject.Find("Point (3)");
        points[3] = GameObject.Find("Point (4)");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            randomNumber = Random.Range(1, 5);

            if(randomNumber == 1)
            {
                collision.gameObject.transform.position = points[0].transform.position;
            }

            if (randomNumber == 2)
            {
                collision.gameObject.transform.position = points[1].transform.position;
            }

            if (randomNumber == 3)
            {
                collision.gameObject.transform.position = points[2].transform.position;
            }

            if (randomNumber == 4)
            {
                collision.gameObject.transform.position = points[3].transform.position;
            }
        }
    }
}
