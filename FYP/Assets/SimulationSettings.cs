using UnityEngine;

[CreateAssetMenu(fileName = "SimulationSettings", menuName = "Scriptable Objects/SimulationSettings")]
public class SimulationSettings : ScriptableObject
{
    [HideInInspector] public float SimSpeed;
    [SerializeField] private int SimulationSpeed;
    [HideInInspector] public int GameState;
    public int Player1Score;
    public int Player2Score;

    void Start()
    {
        SimSpeed = SimulationSpeed * Time.deltaTime;

    }
    
}