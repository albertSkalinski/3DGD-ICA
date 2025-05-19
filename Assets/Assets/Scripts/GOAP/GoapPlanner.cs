using System.Collections.Generic;
using UnityEngine;

public class GoapPlanner
{
    public Queue<GoapAction> Plan(List<GoapAction> availableActions, Dictionary<string, bool> worldState, Dictionary<string, bool> goal)
    {
        List<GoapAction> usableActions = new();

        // Filter usable actions based on procedural checks
        foreach (GoapAction action in availableActions)
        {
            action.DoReset();

            if (action.CheckProceduralPrecondition(null))
                usableActions.Add(action);
        }

        foreach (GoapAction action in usableActions)
        {
            if (HasRequiredPreconditions(worldState, action.preconditions))
            {
                if (ActionAchievesGoal(action, goal))
                {
                    Queue<GoapAction> result = new();
                    result.Enqueue(action);
                    return result;
                }
            }
        }

        return null; // No valid plan
    }

    private bool HasRequiredPreconditions(Dictionary<string, bool> current, Dictionary<string, bool> preconds)
    {
        foreach (var p in preconds)
        {
            if (!current.ContainsKey(p.Key) || current[p.Key] != p.Value)
                return false;
        }
        return true;
    }

    private bool ActionAchievesGoal(GoapAction action, Dictionary<string, bool> goal)
    {
        foreach (var g in goal)
        {
            if (action.effects.ContainsKey(g.Key) && action.effects[g.Key] == g.Value)
                return true;
        }
        return false;
    }
}