using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameControler : MonoBehaviour
{

    public Scrollbar meter;

    [SerializeField] private float drainRate = 0.1f; // amount lost per second4
    [SerializeField] private float gainAmount = 0.05f; // amount gained per button press
    [SerializeField] private float MeterStartingsize = 0.1f;
    [SerializeField] private KeyCode SpamButton;
    [SerializeField] private FightController fightcontroler;
    private PlayerController thisPlayer;
    private float minigameValue;
    public bool CanStartMinigame = false;
    public void StartMinigame(PlayerController Player)
    {
        minigameValue = MeterStartingsize;
        meter.size = minigameValue;
        thisPlayer = Player;
        CanStartMinigame = true;
    }
    public void PressSpace()
    {
        if (CanStartMinigame)
        {
            minigameValue += gainAmount;
            meter.size = minigameValue;
            //Debug.Log("PressSpace: " + meter.size);
        }
    }
    void Update()//optimize with couroutine
    {
        if (CanStartMinigame)
        {
            // Drain over time
            minigameValue -= drainRate * Time.deltaTime;
            meter.size = minigameValue;

            // Button press fills the meter
            if (Input.GetKeyDown(SpamButton))
            {
                minigameValue += gainAmount;
                meter.size = minigameValue;
            }

            // Keep between 0 and 1
            minigameValue = Mathf.Clamp01(meter.size);
            meter.size = minigameValue;
            // Debug.Log(meter.size.ToString("F10"));
            if (minigameValue <= 0)
            {
                fightcontroler.RoundOver(thisPlayer);
                CanStartMinigame = false;
                Debug.Log("FAILED");
            }
            else if (minigameValue >= 1)
            {
                Debug.Log("SUCCESS");
                fightcontroler.Getbackup(thisPlayer);
                CanStartMinigame = false;
            }
        }
    }
}