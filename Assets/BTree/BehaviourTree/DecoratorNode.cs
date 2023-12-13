using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DecoratorNode : Node
{
    [SerializeField]
    protected Node child;
    public Node m_child { get { return child; } set { child = value; } }
    public override Node Clone()
    {
        DecoratorNode node = Instantiate(this);
        node.m_child = child;
        return node;
    }

}
