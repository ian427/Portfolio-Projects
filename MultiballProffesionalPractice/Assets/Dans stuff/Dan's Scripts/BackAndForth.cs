using Unity.VisualScripting;
using UnityEngine;

public class BackAndForth : MonoBehaviour
{
    [SerializeField] private GameObject[] positions;
    [SerializeField] private Transform[] points;
    private float objectSpeed = 2f;
    private int current;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        current = 0;

        positions[0] = GameObject.Find("FirstPoint");
        positions[1] = GameObject.Find("SecondPoint");

        points[0] = positions[0].transform;
        points[1] = positions[1].transform;
    }

    // Update is called once per frame
    void Update()
    {
        Moving();
    }

    private void Moving()
    {
        Vector3 direction = points[current].position - transform.position;

        if (transform.position != points[current].position)
        {
            transform.position = Vector3.MoveTowards(transform.position, points[current].position, objectSpeed * Time.deltaTime);
        }

        else
        {
            current = (current + 1) % points.Length;
        }
    }
}
