using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
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
    public NodeView(Node node)
    {
        this.node = node;
        this.title = node.name;  // Titre du n�ud dans l'�diteur
        this.viewDataKey = node.guid;  // Cl� unique pour la persistance de l'�tat de la vue
        style.left = node.position.x;  // Position horizontale
        style.top = node.position.y;   // Position verticale

        // Cr�ation des ports d'entr�e et de sortie
        CreateInputPorts();
        CreateOutputPorts();
    }

    // Cr�e des ports d'entr�e en fonction du type de n�ud
    private void CreateInputPorts()
    {
        if (node is ActionNode)
        {
            input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));
        }
        else if (node is CompisiteNode)
        {
            input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));

        }
        else if (node is DecoratorNode)
        {
            input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));

        }
        else if (node is RootNode)
        {
            input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));

        }
        if (input != null)
        {
            input.portName = "";
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
            output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(bool));
        }
        else if (node is DecoratorNode)
        {
            output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));

        }
        else if (node is RootNode)
        {
            output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));

        }

        if (output != null)
        {
            output.portName = "";
            outputContainer.Add(output);
        }
    }

    // Met � jour la position du n�ud dans l'arbre comportemental lors du d�placement dans l'�diteur
    public override void SetPosition(Rect newPos)
    {
        base.SetPosition(newPos);
        node.position.x = newPos.xMin;
        node.position.y = newPos.yMin;
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
}
