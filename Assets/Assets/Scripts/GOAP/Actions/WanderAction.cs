using UnityEngine;
using UnityEngine.AI;

public class WanderAction : GoapAction
{
    private float wanderRadius = 8f;
    private Vector3 destination;
    private NavMeshAgent agent;

    private void Start()
    {
        AddEffect("HasWandered", true);
        agent = GetComponent<NavMeshAgent>();
    }

    public override bool CheckProceduralPrecondition(GameObject agentObj)
    {
        // Pick a random point on the NavMesh near current location
        Vector3 randomDir = Random.insideUnitSphere * wanderRadius;
        randomDir += transform.position;

        if (NavMesh.SamplePosition(randomDir, out NavMeshHit hit, wanderRadius, NavMesh.AllAreas))
        {
            destination = hit.position;
            return true;
        }

        return false;
    }

    public override bool Perform(GameObject agentObj)
    {
        agent.SetDestination(destination);
        Debug.Log($"{agent.name} is wandering...");

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            isDone = true;
        }

        return true;
    }

    public override bool IsDone()
    {
        return isDone;
    }
}