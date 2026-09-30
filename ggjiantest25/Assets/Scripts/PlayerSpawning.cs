using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpawning : MonoBehaviour
{
    [SerializeField] float StartTime = 0f;

    public Rigidbody2D Rigidbody;
    public TMP_Text text;

    private float orignalGravity;
    private float countdowntimer;

    // Start is called before the first frame update
    void Start()
    {
        orignalGravity = Rigidbody.gravityScale;
        Rigidbody.gravityScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > StartTime)
        {
            Rigidbody.gravityScale = orignalGravity;
            text.text = "";
        }
        else
        {
            countdowntimer = StartTime - Time.time;

            int timertext = (int)countdowntimer;

            text.text = $"{timertext}";
        }
    }
}
