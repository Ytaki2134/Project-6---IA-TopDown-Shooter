using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

using UnityEngine.InputSystem.HID;

[CreateAssetMenu()]
public class BehaviourTree : ScriptableObject
{
    public Node rootNode;
    public Node.State treeState = Node.State.Running;
    public List<Node> nodes = new List<Node>();

    public Node.State Update()
    {
        if(rootNode.state == Node.State.Running)
        {
            treeState = rootNode.Update();
        }
        return treeState;
    }

    public Node CreateNode(System.Type type)
    {
        Node node = ScriptableObject.CreateInstance(type) as Node;
        node.name = type.Name;
        node.guid = GUID.Generate().ToString(); 
        nodes.Add(node);

        AssetDatabase.AddObjectToAsset(node, this);
        AssetDatabase.SaveAssets();
        return node;

    }

    public void DeleteNode(Node node) { 
        nodes.Remove(node);
        AssetDatabase.RemoveObjectFromAsset(node);
        AssetDatabase.SaveAssets();
    }

    public void AddChild(Node parent, Node child)
    {
        DecoratorNode decorator = parent as DecoratorNode;
        if (decorator)
        {
            decorator.m_child = (child) ;
        }

        RootNode rootNode = parent as RootNode;
        if (rootNode)
        {
            rootNode.m_child = (child);
        }


        CompisiteNode composite = parent as CompisiteNode;
        if (composite)
        {
            composite.AddChildren(child);
        }
    }

    public void RemoveChild(Node parent, Node child) {
        DecoratorNode decorator = parent as DecoratorNode;
        if (decorator)
        {
            decorator.m_child = null;
        }

        RootNode rootNode = parent as RootNode;
        if (rootNode)
        {
            rootNode.m_child = (null);
        }


        CompisiteNode composite = parent as CompisiteNode;
        if (composite)
        {
            composite.RemoveChildren(child);
        }
    }

    public List<Node> GetChildren(Node parent)
    {
        List<Node> children = new List<Node>();
        DecoratorNode decorator = parent as DecoratorNode;
        if (decorator && decorator.m_child != null)
        {
            children.Add(decorator.m_child);
        }
        RootNode rootNode = parent as RootNode;
        if (rootNode && rootNode.m_child != null)
        {
            children.Add(rootNode.m_child);
        }


        CompisiteNode composite = parent as CompisiteNode;
        if (composite)
        {
            return composite.m_children;
        }
        return children;
    }

    public BehaviourTree Clone()
    {
        BehaviourTree tree = Instantiate(this);
        tree.rootNode = tree.rootNode.Clone();
        return tree;
    }
}
