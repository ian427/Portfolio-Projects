using UnityEngine;
using System.Collections;

public class VictoryTrigger : MonoBehaviour
{
    private Vector3 MyPos;
    
    public SimulationSettings settings;
    private bool IsScoreCheckDone = false;
    private int NumberofPlayer1Units = 0;
    private int NumberofPlayer2Units = 0;

    void Start()
    {
        MyPos = this.gameObject.transform.position;
        settings.Player1Score = 0;
    }
    void Update()
    {
        if ((settings.GameState == 0)&& (!IsScoreCheckDone))
        {
            UpdateScore();
           
        }
        else if (settings.GameState == 1)
        {
            IsScoreCheckDone = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player1")
        {
            NumberofPlayer1Units++;
        }
        else if (other.gameObject.tag == "Player2")
        {
            NumberofPlayer2Units++;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player1")
        {
            NumberofPlayer1Units--;
        }
        else if (other.gameObject.tag == "Player2")
        {
            NumberofPlayer2Units--;
        }
    }
     
    private void UpdateScore()
    {
        if (IsScoreCheckDone == false)
        {
            if (NumberofPlayer1Units > NumberofPlayer2Units)
            {
                settings.Player1Score++;
            }
            else if (NumberofPlayer2Units < NumberofPlayer1Units)
            {
                settings.Player2Score++;
            }
            IsScoreCheckDone = true;
        }
        

    }
}
