using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using static Unity.VisualScripting.Metadata;
#endif

public class ParallelNode : CompisiteNode
{
    public enum Policy
    {
        RequireOne,
        RequireAll
    }

    private Policy successPolicy;
    private Policy failurePolicy;

    /*    public ParallelNode(Policy successPolicy, Policy failurePolicy)
        {
            this.successPolicy = successPolicy;
            this.failurePolicy = failurePolicy;
        }
    */

    protected override void OnStart()
    {
        this.successPolicy = Policy.RequireAll;
        this.failurePolicy = Policy.RequireOne;
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        int successCount = 0;
        int failureCount = 0;

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
                    failureCount++;
                    if (failurePolicy == Policy.RequireOne)
                        return State.Failure;
                    break;
            }
        }

        if (successPolicy == Policy.RequireAll && successCount == m_children.Count)
            return State.Success;

        if (failurePolicy == Policy.RequireAll && failureCount == m_children.Count)
            return State.Failure;

        return State.Running;
    }
}
