using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
// Classe NodeView
// Cette classe est une extension de la classe Node d'Unity Editor et représente la visualisation graphique d'un nœud dans l'éditeur d'arbre comportemental.
// Elle gère la création et la configuration des ports d'entrée et de sortie, la position et la sélection du nœud.
public class NodeView : UnityEditor.Experimental.GraphView.Node
{
    public Action<NodeView> m_OnNodeSelected { get; set; }  // Action déclenchée lors de la sélection de ce nœud
    public Node node;  // Référence au nœud de l'arbre comportemental sous-jacent
    public Port input, output;  // Ports d'entrée et de sortie pour la connexion aux autres nœuds

    // Constructeur
    // Initialise la vue du nœud avec les données du nœud de l'arbre comportemental.
    public NodeView(Node node)
    {
        this.node = node;
        this.title = node.name;  // Titre du nœud dans l'éditeur
        this.viewDataKey = node.guid;  // Clé unique pour la persistance de l'état de la vue
        style.left = node.position.x;  // Position horizontale
        style.top = node.position.y;   // Position verticale

        // Création des ports d'entrée et de sortie
        CreateInputPorts();
        CreateOutputPorts();
    }

    // Crée des ports d'entrée en fonction du type de nœud
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

    // Crée des ports de sortie en fonction du type de nœud
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

    // Met à jour la position du nœud dans l'arbre comportemental lors du déplacement dans l'éditeur
    public override void SetPosition(Rect newPos)
    {
        base.SetPosition(newPos);
        node.position.x = newPos.xMin;
        node.position.y = newPos.yMin;
    }
    // Gère l'événement de sélection du nœud
    public override void OnSelected()
    {
        base.OnSelected();
        if(m_OnNodeSelected != null)
        {
            m_OnNodeSelected.Invoke(this);
        }
    }
}
