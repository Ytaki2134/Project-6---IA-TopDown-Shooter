using System;
using UnityEngine;


public class GuardConditionNode : DecoratorNode
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
        if (!condition.Evaluate(blackboard))
        {
            return State.Failure;
        }
      
            return m_child.Update();
    }
}
