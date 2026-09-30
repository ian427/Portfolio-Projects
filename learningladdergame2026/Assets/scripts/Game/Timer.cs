using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Timer : MonoBehaviour
{
    [Header("enter round time in seconds")]
    [SerializeField] private int Roundtime;
    [SerializeField] private FightController FightController;
    private int CurrentSeconds = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void starttimmer ()
    {
        CurrentSeconds = 0;
        StartCoroutine(Clock());
    }
    IEnumerator Clock()
    {
        while (CurrentSeconds < Roundtime) 
        {
            CurrentSeconds++;
            yield return new WaitForSeconds(1f);
                 
        }
        FightController.OutOfTime();

    }
}
