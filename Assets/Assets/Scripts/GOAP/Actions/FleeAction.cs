using UnityEngine;
using UnityEngine.AI;

public class FleeAction : GoapAction
{
    private NavMeshAgent agent;
    private Vector3 fleeTarget;
    private float fleeDistance = 6f;

    private void Start()
    {
        AddPrecondition("PlayerNearby", true);
        AddEffect("IsSafe", true);
        agent = GetComponent<NavMeshAgent>();
    }

    public override bool CheckProceduralPrecondition(GameObject agentObj)
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (!player) return false;

        Vector3 fleeDir = (transform.position - player.transform.position).normalized;
        fleeTarget = transform.position + fleeDir * fleeDistance;

        // Ensure fleeTarget is on the NavMesh
        return NavMesh.SamplePosition(fleeTarget, out NavMeshHit hit, 2f, NavMesh.AllAreas);
    }

    public override bool Perform(GameObject agentObj)
    {
        agent.SetDestination(fleeTarget);
        Debug.Log($"{agent.name} is fleeing...");

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            isDone = true;
        }

        return true;
    }

    public override bool IsDone() => isDone;

    public override void DoReset()
    {
        base.DoReset();
        fleeTarget = Vector3.zero;
    }
}