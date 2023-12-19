using System;
using UnityEngine;


public class GuardConditionNode : ActionNode
{
    public Condition condition;

    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if (condition.Evaluate(blackboard))
        {
            return State.Running;
        }
        else 
            return State.Failure;
    }
}
