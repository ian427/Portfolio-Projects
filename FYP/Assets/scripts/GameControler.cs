using UnityEngine;
using System.Collections;

public class GameControler : MonoBehaviour
{
    public SimulationSettings settings;
    public bool Clock = true;

    private enum GameState
    {
        SetMovement,
        ExecuteMovment,
        Think
        // Add more states here in the future, and the FSM will still loop correctly
    }

    private GameState state;

    void Start()
    {
        state = GameState.SetMovement;
        settings.GameState = (int)state;
    }

    void Update()
    {
        if (Clock)
        {
            StartCoroutine(Timer());
        }

        // These if-statements are kept for future expansion (e.g., logging, triggering events)
        if (state == GameState.SetMovement)
        {
            settings.GameState = (int)state;
        }
        else if (state == GameState.ExecuteMovment)
        {
            settings.GameState = (int)state;
        }
        else if (state == GameState.Think)
        {
            settings.GameState = (int)state;
        }

        /*
        // Cleaner version if you're not using conditions for anything special
        settings.GameState = (int)state;
        */
    }

    private IEnumerator Timer()
    {
        Clock = false;

        yield return new WaitForSeconds(settings.SimSpeed);

        // Advance state in a way that works even if more states are added
        int nextState = ((int)state + 1) % System.Enum.GetValues(typeof(GameState)).Length;
        state = (GameState)nextState;

        Clock = true;
    }
}