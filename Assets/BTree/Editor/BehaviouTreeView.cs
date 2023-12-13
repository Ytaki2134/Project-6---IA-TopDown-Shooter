using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using UnityEditor;
using System.Diagnostics;
using System;
using System.Linq;
public class BehaviouTreeView : GraphView
{
   // private Action<NodeView> OnNodeSelected;
    public Action<NodeView> m_OnNodeSelected { get; set; }

    public new class UxmlFactory : UxmlFactory<BehaviouTreeView, GraphView.UxmlTraits> { }
    BehaviourTree tree;
    public BehaviouTreeView()
    {
       Insert(0, new GridBackground());

        this.AddManipulator(new ContentDragger());

        this.AddManipulator(new ContentZoomer());

        this.AddManipulator(new SelectionDragger());

        this.AddManipulator(new RectangleSelector());
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/BTree/Editor/BehaviourTreeEditor.uss");
        Debug.Assert(styleSheet != null, "StyleSheet is null!");
        styleSheets.Add(styleSheet);
       
    }
    NodeView FindNodeView(Node node)
    {
        NodeView n = GetNodeByGuid(node.guid) as NodeView;
        string s = node.guid;
        return GetNodeByGuid(node.guid) as NodeView;
    }
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
        //Create Nodes
        tree.nodes.ForEach(n => CreateNodeView(n));
        //Create Edges
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
    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        return ports.ToList().Where(endPort => endPort.direction != startPort.direction && endPort.node !=startPort.node).ToList();
    }

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
            return graphViewChange;
    }

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

    void CreateNode(System.Type type)
    {
       Node node = tree.CreateNode(type);
        CreateNodeView(node);
    }

    void CreateNodeView(Node node)
    {
        NodeView nodeView = new NodeView(node);
        nodeView.m_OnNodeSelected = m_OnNodeSelected;
        AddElement(nodeView);
    }
}

