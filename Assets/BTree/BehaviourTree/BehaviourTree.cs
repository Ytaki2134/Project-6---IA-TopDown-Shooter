using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

using UnityEngine.InputSystem.HID;
// Classe BehaviourTree
// Cette classe représente un arbre comportemental dans un système d'IA. 
// Elle gère les nœuds de l'arbre et leurs interactions, permettant la création, la mise à jour, et la suppression de nœuds.
[CreateAssetMenu()]
public class BehaviourTree : ScriptableObject
{
    public Node rootNode;  // Le nœud racine de l'arbre
    public Node.State treeState = Node.State.Running;  // L'état actuel de l'arbre
    public List<Node> nodes = new List<Node>();  // Liste de tous les nœuds dans l'arbre

    // Fonction Update
    // Met à jour l'arbre comportemental en commençant par le nœud racine et retourne l'état actuel de l'arbre.
    public Node.State Update()
    {
        if (rootNode.state == Node.State.Running)
        {
            treeState = rootNode.Update();
        }
        return treeState;
    }

    // Fonction CreateNode
    // Crée un nouveau nœud de type spécifié, l'ajoute à l'arbre et l'enregistre dans la base de données d'assets.
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
    // Supprime un nœud spécifié de l'arbre et de la base de données d'assets.
    public void DeleteNode(Node node)
    {
        nodes.Remove(node);
        AssetDatabase.RemoveObjectFromAsset(node);
        AssetDatabase.SaveAssets();
    }

    // Fonction AddChild
    // Ajoute un nœud enfant à un nœud parent spécifié dans l'arbre.
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
    // Supprime un nœud enfant d'un nœud parent spécifié dans l'arbre.
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
    // Retourne une liste des nœuds enfants d'un nœud parent spécifié.
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
    // Crée et retourne une copie profonde de l'arbre comportemental.
    public BehaviourTree Clone()
    {
        BehaviourTree tree = Instantiate(this);
        tree.rootNode = tree.rootNode.Clone();
        return tree;
    }
}
