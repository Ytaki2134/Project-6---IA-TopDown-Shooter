using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CompisiteNode : Node
{
    [SerializeField]
    protected List<Node> children = new List<Node>();

    public void AddChildren(Node node) { children.Add(node); }
    public void RemoveChildren(Node node) { children.Remove(node); }
    public List<Node> m_children { get { return children; } set { children = value; } }

    public override Node Clone()
    {
        CompisiteNode node = Instantiate(this);
        node.m_children = children.ConvertAll(c => c.Clone());
        return node;
    }

}
