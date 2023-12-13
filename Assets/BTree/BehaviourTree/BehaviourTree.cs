using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

using UnityEngine.InputSystem.HID;
// Classe BehaviourTree
// Cette classe repr�sente un arbre comportemental dans un syst�me d'IA. 
// Elle g�re les n�uds de l'arbre et leurs interactions, permettant la cr�ation, la mise � jour, et la suppression de n�uds.
[CreateAssetMenu()]
public class BehaviourTree : ScriptableObject
{
    public Node rootNode;  // Le n�ud racine de l'arbre
    public Node.State treeState = Node.State.Running;  // L'�tat actuel de l'arbre
    public List<Node> nodes = new List<Node>();  // Liste de tous les n�uds dans l'arbre

    // Fonction Update
    // Met � jour l'arbre comportemental en commen�ant par le n�ud racine et retourne l'�tat actuel de l'arbre.
    public Node.State Update()
    {
        if (rootNode.state == Node.State.Running)
        {
            treeState = rootNode.Update();
        }
        return treeState;
    }

    // Fonction CreateNode
    // Cr�e un nouveau n�ud de type sp�cifi�, l'ajoute � l'arbre et l'enregistre dans la base de donn�es d'assets.
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

    // Fonction DeleteNode
    // Supprime un n�ud sp�cifi� de l'arbre et de la base de donn�es d'assets.
    public void DeleteNode(Node node)
    {
        nodes.Remove(node);
        AssetDatabase.RemoveObjectFromAsset(node);
        AssetDatabase.SaveAssets();
    }

    // Fonction AddChild
    // Ajoute un n�ud enfant � un n�ud parent sp�cifi� dans l'arbre.
    public void AddChild(Node parent, Node child)
    {
        DecoratorNode decorator = parent as DecoratorNode;
        if (decorator)
        {
            decorator.m_child = (child);
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

    // Fonction RemoveChild
    // Supprime un n�ud enfant d'un n�ud parent sp�cifi� dans l'arbre.
    public void RemoveChild(Node parent, Node child)
    {
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

    // Fonction GetChildren
    // Retourne une liste des n�uds enfants d'un n�ud parent sp�cifi�.
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

    // Fonction Clone
    // Cr�e et retourne une copie profonde de l'arbre comportemental.
    public BehaviourTree Clone()
    {
        BehaviourTree tree = Instantiate(this);
        tree.rootNode = tree.rootNode.Clone();
        return tree;
    }
}
