using UnityEngine;
using System.Collections;

public class UnitSightGrid : MonoBehaviour
{
    private Vector3 MyPos;
    public float HeatNumber = 0;
    public SimulationSettings settings;
    private bool SomethingInTrigger = false;
    private bool UpdateIsDone = false;

    void Start()
    {
        MyPos = this.gameObject.transform.position;
    }
    void Update()//lowers cell over time
    {
        if( (settings.GameState == 0)&& (!UpdateIsDone))
        {
            //CheckScore();
            if (HeatNumber != 0)
            {
                if (HeatNumber < 0)
                {
                    HeatNumber += 0.5f;
                }
                else if (HeatNumber > 0)
                {
                    HeatNumber -= 0.5f;
                }
            }
        }
        else if (settings.GameState == 1)
        {
            UpdateIsDone = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        SomethingInTrigger = true;
        CheckScore(other);

    }
    private void OnTriggerExit(Collider other)
    {
        SomethingInTrigger = false;
    }
    private void CheckScore (Collider other)
    {
        if ((SomethingInTrigger)&&(!UpdateIsDone))
        {
             if (other.gameObject.tag == "Player1")
             {
                 HeatNumber += 0.5f;
             }
             else if (other.gameObject.tag == "Player2")
             {
                HeatNumber -= 0.5f;
             }

        }
        UpdateIsDone = true;
    }
}
