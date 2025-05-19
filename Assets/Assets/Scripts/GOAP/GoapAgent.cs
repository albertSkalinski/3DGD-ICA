using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WorldState))]
public class GoapAgent : MonoBehaviour
{
    private WorldState worldState;
    private GoapPlanner planner = new();

    private List<GoapAction> availableActions = new();
    private Queue<GoapAction> currentPlan;

    private Dictionary<string, bool> currentGoal;

    private int wanderCount = 0;
    private int wanderBeforeTired = 3;

    void Start()
    {
        worldState = GetComponent<WorldState>();

        // Collect all attached actions
        foreach (GoapAction action in GetComponents<GoapAction>())
        {
            availableActions.Add(action);
        }

        // Set default world state
        worldState.SetState("IsTired", false);
        worldState.SetState("IsRested", false);
        worldState.SetState("PlayerNearby", false);
        worldState.SetState("IsSafe", true);
    }

    void Update()
    {
        SenseEnvironment(); // Check nearby world facts

        if (currentPlan == null || currentPlan.Count == 0)
        {
            currentGoal = CreateGoal(); // Set next goal
            currentPlan = planner.Plan(availableActions, worldState.states, currentGoal);
        }

        if (currentPlan != null && currentPlan.Count > 0)
        {
            GoapAction action = currentPlan.Peek();

            if (action.IsDone())
            {
                ApplyEffects(action);
                currentPlan.Dequeue();
            }
            else
            {
                action.Perform(gameObject);
            }
        }
    }

    private Dictionary<string, bool> CreateGoal()
    {
        // Priority 1: Flee if player is nearby
        if (worldState.GetState("PlayerNearby"))
            return new Dictionary<string, bool> { { "IsSafe", true } };

        // Priority 2: Rest if tired
        if (worldState.GetState("IsTired"))
            return new Dictionary<string, bool> { { "IsRested", true } };

        // Default: wander
        return new Dictionary<string, bool> { { "HasWandered", true } };
    }

    private void ApplyEffects(GoapAction action)
    {
        foreach (var effect in action.effects)
        {
            worldState.SetState(effect.Key, effect.Value);
        }

        if (action is WanderAction)
        {
            wanderCount++;
            if (wanderCount >= wanderBeforeTired)
            {
                worldState.SetState("IsTired", true);
            }
        }
        else if (action is RestAction)
        {
            wanderCount = 0;
            worldState.SetState("IsTired", false);
            worldState.SetState("IsRested", true);
        }
    }

    private void SenseEnvironment()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (!player) return;

        float distance = Vector3.Distance(transform.position, player.transform.position);
        worldState.SetState("PlayerNearby", distance < 6f);
    }
}