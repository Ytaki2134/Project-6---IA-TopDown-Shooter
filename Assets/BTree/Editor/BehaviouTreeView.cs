using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using UnityEditor;
using System.Diagnostics;
using System;
using System.Linq;
// Classe BehaviourTreeView
// Cette classe est une extension de GraphView dans Unity et sert � visualiser et � manipuler graphiquement des arbres comportementaux.
// Elle offre des fonctionnalit�s telles que le d�placement, le zoom, la s�lection et la connexion des n�uds.
public class BehaviouTreeView : GraphView
{
    public Action<NodeView> m_OnNodeSelected { get; set; }  // Action d�clench�e lors de la s�lection d'un n�ud

    public new class UxmlFactory : UxmlFactory<BehaviouTreeView, GraphView.UxmlTraits> { }  // Factory pour l'interface utilisateur XML
    BehaviourTree tree;  // R�f�rence � l'arbre comportemental actuel

    public BehaviouTreeView()
    {
        // Initialisation de l'interface graphique
        Insert(0, new GridBackground());

        // Ajout de manipulations de base (d�placement, zoom, s�lection, etc.)
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new ContentZoomer());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        // Chargement et application de la feuille de style
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/BTree/Editor/BehaviourTreeEditor.uss");
        Debug.Assert(styleSheet != null, "StyleSheet is null!");
        styleSheets.Add(styleSheet);

        Undo.undoRedoPerformed += OnUndoRedo;
    }

    private void OnUndoRedo()
    {
        PopulateView(tree);
        AssetDatabase.SaveAssets();
    }

    // Trouve une vue de n�ud bas�e sur le n�ud de l'arbre comportemental
    NodeView FindNodeView(Node node)
    {
        return GetNodeByGuid(node.guid) as NodeView;
    }

    // Remplit la vue avec les n�uds de l'arbre comportemental
    internal void PopulateView(BehaviourTree tree)
    {
        this.tree = tree;
        graphViewChanged -= OnGraphViewChanged;
        DeleteElements(graphElements);
        graphViewChanged += OnGraphViewChanged;

        if(tree.rootNode == null)
        {
            tree.rootNode = tree.CreateNode(typeof(RootNode)) as RootNode;
            EditorUtility.SetDirty(tree);
            AssetDatabase.SaveAssets();
        }

        // Cr�ation des vues de n�ud
        tree.nodes.ForEach(n => CreateNodeView(n));

        // Cr�ation des connexions entre les n�uds
        tree.nodes.ForEach(n =>
        {
            var children = tree.GetChildren(n);
            children.ForEach(c =>
            {
                NodeView childView = FindNodeView(c);
                NodeView parentView = FindNodeView(n);
                Edge edge = parentView.output.ConnectTo(childView.input);
                AddElement(edge);
            });
        });
    }

    // D�termine les ports compatibles pour la connexion
    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        return ports.ToList().Where(endPort => endPort.direction != startPort.direction && endPort.node != startPort.node).ToList();
    }

    // G�re les changements sur la vue graphique, comme la suppression ou l'ajout de connexions

    private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
    {
        if(graphViewChange.elementsToRemove != null)
        {
            graphViewChange.elementsToRemove.ForEach(elem =>
            {
                NodeView nodeView = elem as NodeView;
                if (nodeView != null)
                {
                    tree.DeleteNode(nodeView.node);
                }

                Edge edge = elem as Edge;
                if (edge != null)
                {
                    NodeView parentView = edge.output.node as NodeView;
                    NodeView childView = edge.input.node as NodeView;
                    tree.RemoveChild(parentView.node, childView.node);
                }
            });
        }

        if (graphViewChange.edgesToCreate != null)
        {
            graphViewChange.edgesToCreate.ForEach(edge =>
            {
                NodeView parentView = edge.output.node as NodeView;
                NodeView childView = edge.input.node as NodeView;
                tree.AddChild(parentView.node, childView.node);
            });
        }
        if(graphViewChange.movedElements != null)
        {
            nodes.ForEach(node =>
            {
                NodeView view = node as NodeView;
                view.SortChildren();
            });
        }
        return graphViewChange;
    }

    // Construit le menu contextuel pour ajouter de nouveaux n�uds
    public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
    {
        //base.BuildContextualMenu(evt);
        {
            var types = TypeCache.GetTypesDerivedFrom<ActionNode>();
            foreach (var type in types)
            {
                evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", (a) => CreateNode(type));
            }
            var typesC = TypeCache.GetTypesDerivedFrom<CompisiteNode>();
            foreach (var type in typesC)
            {
                evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", (a) => CreateNode(type));
            }
            var typesD = TypeCache.GetTypesDerivedFrom<DecoratorNode>();
            foreach (var type in typesD)
            {
                evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", (a) => CreateNode(type));
            }
        }
    }
    // Cr�e un nouveau n�ud de type sp�cifi�
    void CreateNode(System.Type type)
    {
        Node node = tree.CreateNode(type);
        CreateNodeView(node);
    }

    // Cr�e une vue pour le n�ud sp�cifi�
    void CreateNodeView(Node node)
    {
        NodeView nodeView = new NodeView(node);
        nodeView.m_OnNodeSelected = m_OnNodeSelected;
        AddElement(nodeView);
    }

    public void UpdateNodeStates()
    {
        nodes.ForEach(n =>
        {
            NodeView view = n as NodeView;
            view.UpdateState();
        });
    }
}

