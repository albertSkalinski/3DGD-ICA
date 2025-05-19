using UnityEngine;

public class RestAction : GoapAction
{
    private float restDuration = 1f;
    private float startTime;

    private void Start()
    {
        AddPrecondition("IsTired", true);        // Only rest when tired
        AddEffect("IsRested", true);             // Outcome of resting
    }

    public override bool CheckProceduralPrecondition(GameObject agent)
    {
        // No conditions required in environment (not world-based)
        return true;
    }

    public override bool Perform(GameObject agent)
    {
        if (startTime == 0f)
        {
            startTime = Time.time;
            Debug.Log($"{agent.name} is resting...");
        }

        if (Time.time - startTime >= restDuration)
        {
            isDone = true;
        }

        return true;
    }

    public override bool IsDone()
    {
        return isDone;
    }

    public override void DoReset()
    {
        base.DoReset();
        startTime = 0f;
    }
}