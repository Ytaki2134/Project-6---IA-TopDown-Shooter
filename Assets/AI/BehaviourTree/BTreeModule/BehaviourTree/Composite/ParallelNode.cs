using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ParallelNode : CompisiteNode
{
    public enum Policy
    {
        RequireOne,
        RequireAll
    }
    [SerializeField] private Policy successPolicy;

    protected override void OnStart()
    {

    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        int successCount = 0;

        foreach (Node child in m_children)
        {
            switch (child.Update())
            {
                case State.Running:
                    break;
                case State.Success:
                    successCount++;
                    if (successPolicy == Policy.RequireOne)
                        return State.Success;
                    break;
                case State.Failure:
                    break;
            }
        }

        if (successPolicy == Policy.RequireAll && successCount == m_children.Count)
            return State.Success;
        
        return State.Failure;
    }
}
