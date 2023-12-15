using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Callbacks;
using System;
// Classe BehaviourTreeEditor
// Cette classe est une extension d'EditorWindow dans Unity et sert d'interface utilisateur pour �diter des arbres comportementaux.
// Elle fournit un environnement visuel pour cr�er, modifier et visualiser des arbres comportementaux.
public class BehaviourTreeEditor : EditorWindow
{
    BehaviouTreeView treeView;  // Vue de l'arbre comportemental
    InspectorView inspectorView;  // Vue de l'inspecteur pour modifier les propri�t�s des n�uds
    IMGUIContainer blackboardView;
    SerializedObject treeObject;

    SerializedProperty blackboardProperty;

    // MenuItem pour ouvrir l'�diteur d'arbre comportemental
    [MenuItem("BehaviourTree/Editor ...")]
    public static void OpenWindow()
    {
        BehaviourTreeEditor wnd = GetWindow<BehaviourTreeEditor>();
        wnd.titleContent = new GUIContent("BehaviourTreeEditor");
    }

    [OnOpenAsset]
    public static bool OnOpenAsset(int instanceID, int line)
    {
        if(Selection.activeObject is BehaviourTree)
        {
            OpenWindow();
            return true;
        }
        return false;
    }

    // Fonction CreateGUI
    // Initialise l'interface utilisateur de l'�diteur d'arbre comportemental.
    public void CreateGUI()
    {
        // Initialisation de l'�l�ment racine de l'interface utilisateur
        VisualElement root = rootVisualElement;

        // Chargement et application de l'arbre visuel et de la feuille de style
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/BTreeModule/Editor/BehaviourTreeEditor.uxml");
        visualTree.CloneTree(root);
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/BTreeModule/Editor/BehaviourTreeEditor.uss");
        root.styleSheets.Add(styleSheet);

        // Initialisation des vues de l'arbre et de l'inspecteur
        treeView = root.Q<BehaviouTreeView>();
        inspectorView = root.Q<InspectorView>();
        blackboardView = root.Q<IMGUIContainer>();
        blackboardView.onGUIHandler = () => {
            if (treeObject != null && treeObject.targetObject != null)
            {
                treeObject.Update();
                EditorGUILayout.PropertyField(blackboardProperty);
                treeObject.ApplyModifiedProperties();
            }
        };


        // Configuration des callbacks
        treeView.m_OnNodeSelected = OnNodeSelectionChanged;
        OnSelectionChange();
    }

    private void OnEnable()
    {
        EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    

    private void OnDisable()
    {
        EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
    }
    private void OnPlayModeStateChanged(PlayModeStateChange obj)
    {
        switch(obj)
        {
            case PlayModeStateChange.EnteredEditMode:
                OnSelectionChange();
                break;
            case PlayModeStateChange.ExitingEditMode:
                break;
            case PlayModeStateChange.EnteredPlayMode:
                OnSelectionChange();
                break;
            case PlayModeStateChange.ExitingPlayMode:
                break;
        }
    }
    // Fonction OnSelectionChange
    // Appel�e lorsqu'un arbre comportemental est s�lectionn� pour le charger dans l'�diteur.
    private void OnSelectionChange()
    {
        GameObject selectedObject = Selection.activeGameObject;

        if (selectedObject != null)
        {
            SerializedObject serializedObject = new SerializedObject(selectedObject);
           // inspectorView.UpdateWithSerializedObject(serializedObject);
        }
        BehaviourTree tree = Selection.activeObject as BehaviourTree;
        // Ajoutez des v�rifications de null pour �viter la NullReferenceException.
        if (tree == null && Selection.activeGameObject != null)
        {
            BehaviourTreeRunner runner = Selection.activeGameObject.GetComponent<BehaviourTreeRunner>();
            if (runner && runner.tree != null)
            {
                tree = runner.tree;
            }
        }

        if (tree != null)
        {
            // Assurez-vous �galement que treeView n'est pas null avant d'appeler PopulateView
            if (Application.isPlaying || AssetDatabase.CanOpenAssetInEditor(tree.GetInstanceID()))
            {
                treeView?.PopulateView(tree); // Utilisation de l'op�rateur conditionnel '?'
            }
            treeObject = new SerializedObject(tree);
            blackboardProperty = treeObject.FindProperty("blackboard");
        }
    }


    // Fonction OnNodeSelectionChanged
    // Appel�e lorsqu'un n�ud est s�lectionn� pour mettre � jour la vue de l'inspecteur.
    void OnNodeSelectionChanged(NodeView node)
    {
        inspectorView.UpdateSelection(node);
    }

    private void OnInspectorUpdate()
    {
        treeView?.UpdateNodeStates();
    }
}
