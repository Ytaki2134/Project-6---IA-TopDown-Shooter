using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEditor;
using Unity.VisualScripting;

// Classe NodeView
// Cette classe est une extension de la classe Node d'Unity Editor et repr�sente la visualisation graphique d'un n�ud dans l'�diteur d'arbre comportemental.
// Elle g�re la cr�ation et la configuration des ports d'entr�e et de sortie, la position et la s�lection du n�ud.
public class NodeView : UnityEditor.Experimental.GraphView.Node
{
    public Action<NodeView> m_OnNodeSelected { get; set; }  // Action d�clench�e lors de la s�lection de ce n�ud
    public Node node;  // R�f�rence au n�ud de l'arbre comportemental sous-jacent
    public Port input, output;  // Ports d'entr�e et de sortie pour la connexion aux autres n�uds

    // Constructeur
    // Initialise la vue du n�ud avec les donn�es du n�ud de l'arbre comportemental. 
    public NodeView(Node node) : base("Assets/BTreeModule/Editor/NodeView.uxml")
    {
        this.node = node;
        this.title = node.name;  // Titre du n�ud dans l'�diteur
        this.viewDataKey = node.guid;  // Cl� unique pour la persistance de l'�tat de la vue
        style.left = node.position.x;  // Position horizontale
        style.top = node.position.y;   // Position verticale

        // Cr�ation des ports d'entr�e et de sortie
        CreateInputPorts();
        CreateOutputPorts();

        SetupClasses();
    }

    private void SetupClasses()
    {
        if (node is ActionNode)
        {
            AddToClassList("action");
        }
        else if (node is CompisiteNode)
        {
            AddToClassList("composite");
        }
        else if (node is DecoratorNode)
        {
            AddToClassList("decorator");
        }
        else if (node is RootNode)
        {
            AddToClassList("root");
        }
    }

    // Cr�e des ports d'entr�e en fonction du type de n�ud
    private void CreateInputPorts()
    {
        if (node is ActionNode)
        {
            input = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
        }
        else if (node is CompisiteNode)
        {
            input = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));

        }
        else if (node is DecoratorNode)
        {
            input = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));

        }
        else if (node is RootNode)
        {
           

        }
        if (input != null)
        {
            input.portName = "";
            input.style.flexDirection = FlexDirection.Column;
            inputContainer.Add(input);
        }
    }

    // Cr�e des ports de sortie en fonction du type de n�ud
    private void CreateOutputPorts()
    {
        if(node is ActionNode)
        {
        }
        else if (node is CompisiteNode)
        {
            output = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Multi, typeof(bool));
        }
        else if (node is DecoratorNode)
        {
            output = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Single, typeof(bool));

        }
        else if (node is RootNode)
        {
            output = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Single, typeof(bool));

        }

        if (output != null)
        {
            output.portName = "";
            output.style.flexDirection = FlexDirection.ColumnReverse;

            outputContainer.Add(output);
        }
    }

    // Met � jour la position du n�ud dans l'arbre comportemental lors du d�placement dans l'�diteur
    public override void SetPosition(Rect newPos)
    {
        base.SetPosition(newPos);
        Undo.RecordObject(node, "Behaviour Tree (Set Position)");
        node.position.x = newPos.xMin;
        node.position.y = newPos.yMin;
        EditorUtility.SetDirty(node);
    }
    // G�re l'�v�nement de s�lection du n�ud
    public override void OnSelected()
    {
        base.OnSelected();
        if(m_OnNodeSelected != null)
        {
            m_OnNodeSelected.Invoke(this);
        }
    }

    public void SortChildren()
    {
        CompisiteNode composite = node as CompisiteNode;
        if (composite != null && composite.m_children != null)
        {
            composite.m_children.Sort(SortByHorizontalPosition);
        }
    }


    private int SortByHorizontalPosition(Node left, Node right)
    {
       return left.position.x < right.position.x ? -1 : 1;
    }

    public void UpdateState()
    {
        RemoveFromClassList("running");
        RemoveFromClassList("success");
        RemoveFromClassList("failure");


        if (Application.isPlaying)
        {
            switch (node.state)
            {
                case Node.State.Running:
                    if (node.started)
                    {
                        AddToClassList("running");
                    }
                    break;
                case Node.State.Success:
                    AddToClassList("success");

                    break;
                case Node.State.Failure:
                    AddToClassList("failure");
                    break;
            }
        }
    }
}