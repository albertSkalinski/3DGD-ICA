using System.Collections.Generic;
using UnityEngine;

public abstract class GoapAction : MonoBehaviour
{
    // Cost of this action (used in planning)
    public float cost = 1f;

    // Preconditions and effects
    public Dictionary<string, bool> preconditions = new();
    public Dictionary<string, bool> effects = new();

    // Action lifecycle
    protected bool isDone = false;

    // The target for this action, if any
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

    public abstract bool CheckProceduralPrecondition(GameObject agent);
    public abstract bool Perform(GameObject agent);
    public abstract bool IsDone();
}