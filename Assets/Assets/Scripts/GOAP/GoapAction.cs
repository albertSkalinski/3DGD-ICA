using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract base class for all GOAP actions.
/// This provides structure and utility for planning and executing actions.
/// </summary>

public abstract class GoapAction : MonoBehaviour
{
    public float cost = 1f;

    // Preconditions (have to fulfilled)
    public Dictionary<string, bool> preconditions = new();
    public Dictionary<string, bool> effects = new();

    protected bool isDone = false;

    //For possible future use
    public GameObject target;

    public void AddPrecondition(string key, bool value)
    {
        preconditions[key] = value;
    }

    public void AddEffect(string key, bool value)
    {
        effects[key] = value;
    }

    public virtual void DoReset()
    {
        isDone = false;
        target = null;
    }

    //Checks whether the action is currently achievable given the game world state
    public abstract bool CheckProceduralPrecondition(GameObject agent);
    public abstract bool Perform(GameObject agent);
    public abstract bool IsDone();
}