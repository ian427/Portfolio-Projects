using UnityEngine;
using UnityEngine.Rendering;

public class CirclePulsing : MonoBehaviour
{
    [SerializeField] private Transform pulseTransform;
    private float range;
    private float rangeMax;
    private float rangeMin;

    private bool pulsingOut;
    private float pulseSpeed;
   
    void Start()
    {
        //pulseTransform = transform.Find("Pulse");
        rangeMax = 3f;
        rangeMin = 1f;
        range = 1f;

        pulseSpeed = 2f;
        pulsingOut = false;
    }

    private void Update()
    {
        if(pulsingOut == true)
        {
            IncreasePulse();
        }

        else
        {
            DecreasePulse();
        }

        pulseTransform.localScale = new Vector3(range, range, 1f);
    }

    private void IncreasePulse()
    {
        range += pulseSpeed * Time.deltaTime;

        if (range >= rangeMax)
        {
            range = rangeMax;
            pulsingOut = false;
        }
    }

    private void DecreasePulse()
    {
        range -= pulseSpeed * Time.deltaTime;

        if (range <= rangeMin)
        {
            range = rangeMin;
            pulsingOut = true;
        }
    }
}
