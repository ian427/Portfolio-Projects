using UnityEngine;
using System.Collections;


public class Spawnerbob : MonoBehaviour
{


    [SerializeField] float wiggleAmount = 0.2f;   // how much to move in/out
    [SerializeField] float wiggleSpeed = 2f;      // how fast to wiggle

    float baseRadius;  // original distance from center

    void Start()
    {
        baseRadius = transform.localPosition.magnitude;
    }

    void Update()
    {
        // Calculate radial offset using a sine wave
        float offset = Mathf.Sin(Time.time * wiggleSpeed) * wiggleAmount;

        // New radius = base distance + wiggle offset
        float radius = baseRadius + offset;

        // Keep same angle: normalize keeps direction
        Vector3 dir = transform.localPosition.normalized;

        // Apply radial wiggle
        transform.localPosition = dir * radius;
    }

}