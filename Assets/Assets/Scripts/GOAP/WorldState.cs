using System.Collections.Generic;
using UnityEngine;

public class WorldState : MonoBehaviour
{
    public Dictionary<string, bool> states = new();

    public void SetState(string key, bool value)
    {
        states[key] = value;
    }

    public bool GetState(string key)
    {
        return states.ContainsKey(key) && states[key];
    }
}
