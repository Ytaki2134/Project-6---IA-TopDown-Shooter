using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
// Classe BehaviourTreeEditor
// Cette classe est une extension d'EditorWindow dans Unity et sert d'interface utilisateur pour éditer des arbres comportementaux.
// Elle fournit un environnement visuel pour créer, modifier et visualiser des arbres comportementaux.
public class BehaviourTreeEditor : EditorWindow
{
    BehaviouTreeView treeView;  // Vue de l'arbre comportemental
    InspectorView inspectorView;  // Vue de l'inspecteur pour modifier les propriétés des nœuds

    // MenuItem pour ouvrir l'éditeur d'arbre comportemental
    [MenuItem("BehaviourTree/Editor ...")]
    public static void OpenWindow()
    {
        BehaviourTreeEditor wnd = GetWindow<BehaviourTreeEditor>();
        wnd.titleContent = new GUIContent("BehaviourTreeEditor");
    }

    // Fonction CreateGUI
    // Initialise l'interface utilisateur de l'éditeur d'arbre comportemental.
    public void CreateGUI()
    {
        // Initialisation de l'élément racine de l'interface utilisateur
        VisualElement root = rootVisualElement;

        // Chargement et application de l'arbre visuel et de la feuille de style
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/BTree/Editor/BehaviourTreeEditor.uxml");
        visualTree.CloneTree(root);
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/BTree/Editor/BehaviourTreeEditor.uss");
        root.styleSheets.Add(styleSheet);

        // Initialisation des vues de l'arbre et de l'inspecteur
        treeView = root.Q<BehaviouTreeView>();
        inspectorView = root.Q<InspectorView>();

        // Configuration des callbacks
        treeView.m_OnNodeSelected = OnNodeSelectionChanged;
        OnSelectionChange();
    }

    // Fonction OnSelectionChange
    // Appelée lorsqu'un arbre comportemental est sélectionné pour le charger dans l'éditeur.
    private void OnSelectionChange()
    {
        BehaviourTree tree = Selection.activeObject as BehaviourTree;
        if (tree && AssetDatabase.CanOpenAssetInEditor(tree.GetInstanceID()))
        {
            treeView.PopulateView(tree);
        }
    }

    // Fonction OnNodeSelectionChanged
    // Appelée lorsqu'un nœud est sélectionné pour mettre à jour la vue de l'inspecteur.
    void OnNodeSelectionChanged(NodeView node)
    {
        inspectorView.UpdateSelection(node);
    }
}
