using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequencerNode : CompisiteNode
{
    int current;
    protected override void OnStart()
    {
       current = 0;
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        Node child = children[current];
        switch (child.Update())
        {
            case State.Running: 
                return State.Running;
            case State.Failure: 
                return State.Failure;
            case State.Success:
              
                current++;
                break;

        }
        return current == children.Count ? State.Success : State.Running;   
    }

    public List<Node> GetChildren() { return children; }
}
