using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootNode : Node
{
    private Node child;
    public Node m_child { get { return child; } set { child = value;} }

    public override Node Clone()
    {
        RootNode node = Instantiate(this);
        node.m_child = child;
        return node;
    }

    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        return child.Update();
    }
}
