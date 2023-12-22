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
    public Blackboard blackboard = new Blackboard();
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
#if UNITY_EDITOR
    // Fonction CreateNode
    // Crée un nouveau nœud de type spécifié, l'ajoute à l'arbre et l'enregistre dans la base de données d'assets.
    public Node CreateNode(System.Type type)
    {
        Node node = ScriptableObject.CreateInstance(type) as Node;
        node.name = type.Name;
        node.guid = GUID.Generate().ToString();

        node.blackboard = blackboard;

        Undo.RecordObject(this, "Behaviour Tree (CreateNode)");

        nodes.Add(node);
        if(!Application.isPlaying)
        {
            AssetDatabase.AddObjectToAsset(node, this);

        }
        Undo.RegisterCreatedObjectUndo(node, "Behaviour Tree (CreateNode)");
        AssetDatabase.SaveAssets();
        return node;
    }

    // Fonction DeleteNode
    // Supprime un nœud spécifié de l'arbre et de la base de données d'assets.
    public void DeleteNode(Node node)
    {
        Undo.RecordObject(this, "Behaviour Tree (DeleteNode)");

        nodes.Remove(node);
        //AssetDatabase.RemoveObjectFromAsset(node);
        Undo.DestroyObjectImmediate(node);
        AssetDatabase.SaveAssets();
    }

    // Fonction AddChild
    // Ajoute un nœud enfant à un nœud parent spécifié dans l'arbre.
    public void AddChild(Node parent, Node child)
    {
        DecoratorNode decorator = parent as DecoratorNode;
        if (decorator)
        {
            Undo.RecordObject(decorator, "Behaviour Tree (AddChild)");
            decorator.m_child = (child);
            EditorUtility.SetDirty(decorator);

        }

        RootNode rootNode = parent as RootNode;
        if (rootNode)
        {
            Undo.RecordObject(rootNode, "Behaviour Tree (AddChild)");

            rootNode.m_child = (child);
            EditorUtility.SetDirty(rootNode);

        }

        CompisiteNode composite = parent as CompisiteNode;
        if (composite)
        {
            Undo.RecordObject(composite, "Behaviour Tree (AddChild)");

            composite.AddChildren(child);
            EditorUtility.SetDirty(composite); 
        }
    }

    // Fonction RemoveChild
    // Supprime un nœud enfant d'un nœud parent spécifié dans l'arbre.
    public void RemoveChild(Node parent, Node child)
    {
        DecoratorNode decorator = parent as DecoratorNode;
        if (decorator)
        {
            Undo.RecordObject(decorator, "Behaviour Tree (RemoveChild)");

            decorator.m_child = null;
            EditorUtility.SetDirty(decorator);

        }

        RootNode rootNode = parent as RootNode;
        if (rootNode)
        {
            Undo.RecordObject(rootNode, "Behaviour Tree (RemoveChild)");

            rootNode.m_child = (null);
            EditorUtility.SetDirty(rootNode);

        }

        CompisiteNode composite = parent as CompisiteNode;
        if (composite)
        {
            Undo.RecordObject(composite, "Behaviour Tree (RemoveChild)");

            composite.RemoveChildren(child);
            EditorUtility.SetDirty(composite);

        }
    }

#endif
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


    public void Traverse(Node node, System.Action<Node> visiter)
    {
        if (node)
        {
            visiter.Invoke(node);
            var children = GetChildren(node);
            children.ForEach((n) => Traverse(n, visiter));
        }
    }

    // Fonction Clone
    // Crée et retourne une copie profonde de l'arbre comportemental.
    public BehaviourTree Clone()
    {
        BehaviourTree tree = Instantiate(this);
        tree.rootNode = tree.rootNode.Clone();
        tree.nodes = new List<Node>();
        Traverse(tree.rootNode, (n) => {
            n.blackboard = tree.blackboard; // Assurez-vous que le blackboard est mis à jour
            tree.nodes.Add(n);
        });
        return tree;
    }

}
