using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Selection of a single action to fulfill a given goal,
/// if its preconditions are satisfied by the current world state.
/// </summary>
public class GoapPlanner
{
    public Queue<GoapAction> Plan(List<GoapAction> availableActions, Dictionary<string, bool> worldState, Dictionary<string, bool> goal)
    {
        List<GoapAction> usableActions = new();

        //Filter actions that are currently usable
        foreach (GoapAction action in availableActions)
        {
            action.DoReset();

            if (action.CheckProceduralPrecondition(null))
                usableActions.Add(action);
        }

        //Check if any of the usable actions can achieve the goal
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

        return null;
    }

    //Check if the current world state satisfies the action's preconditions
    private bool HasRequiredPreconditions(Dictionary<string, bool> current, Dictionary<string, bool> preconds)
    {
        foreach (var p in preconds)
        {
            if (!current.ContainsKey(p.Key) || current[p.Key] != p.Value)
                return false;
        }
        return true;
    }

    //Checks whether an action has effects that match any part of the goal
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