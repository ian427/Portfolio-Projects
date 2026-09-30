using UnityEngine;
using UnityEngine.AI;

public class Movetest : MonoBehaviour
{

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Set the destination to (10, 1, 10)
        Vector3 targetPosition = new Vector3(10f, 1f, 10f);
        agent.SetDestination(targetPosition);

        Debug.Log($"Moving to {targetPosition}");
    }

    void Update()
    {
        // Optional: Check if the agent has reached the destination
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            {
                Debug.Log("Arrived at destination.");
            }
        }
    }

}
