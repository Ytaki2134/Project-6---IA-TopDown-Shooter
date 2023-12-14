using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
// Classe InspectorView
// Cette classe est un élément d'interface utilisateur personnalisé pour Unity qui agit comme un inspecteur.
// Elle est utilisée pour afficher et modifier les propriétés de l'objet sélectionné, similaire à l'inspecteur standard de Unity.
public class InspectorView : VisualElement
{
    // Factory pour la création de l'élément InspectorView dans l'interface utilisateur UXML
    public new class UxmlFactory : UxmlFactory<InspectorView, VisualElement.UxmlTraits> { }

    // L'éditeur associé à l'objet sélectionné
    Editor editor;


    // Constructeur par défaut
    public InspectorView()
    {

    }
    public void UpdateWithSerializedObject(SerializedObject serializedObject)
    {
        Clear();

        // Itérer sur les propriétés en utilisant la propriété racine
        SerializedProperty iterator = serializedObject.GetIterator();
        bool enterChildren = true;
        while (iterator.NextVisible(enterChildren))
        {
            // La première fois, nous voulons entrer dans les enfants de la première propriété
            enterChildren = false;

            // Créez un PropertyField pour chaque propriété trouvée
            PropertyField propertyField = new PropertyField(iterator.Copy()) { name = "PropertyField:" + iterator.propertyPath };

            if (iterator.propertyPath == "m_Script" && serializedObject.targetObject != null)
            {
                propertyField.SetEnabled(value: false); // Désactive le champ du script pour qu'il ne soit pas modifiable
            }
            else
            {
                Add(propertyField);
                propertyField.Bind(serializedObject); // Lie la propriété SerializedProperty au SerializedObject
            }
        }
        serializedObject.ApplyModifiedProperties();
    }

    // Méthode UpdateSelection
    // Met à jour l'inspecteur pour afficher les propriétés de l'objet NodeView sélectionné.
    internal void UpdateSelection(NodeView nodeView)
    {
        // Efface les éléments actuels de l'inspecteur
        Clear();

        // Détruit l'éditeur existant pour éviter les fuites de mémoire
        UnityEngine.Object.DestroyImmediate(editor);

        // Crée un nouvel éditeur pour l'objet sélectionné
        editor = Editor.CreateEditor(nodeView.node);

        // Crée et ajoute un conteneur IMGUI pour afficher l'interface de l'éditeur
        IMGUIContainer container = new IMGUIContainer(() => {
            if (editor.target)
            {
                editor.OnInspectorGUI(); 
            }
        });

        // Création d'un SerializedObject à partir de l'objet cible (nodeView.node)
        SerializedObject serializedObject = new SerializedObject(nodeView.node);

        // Création d'un ObjectField pour la propriété que vous voulez modifier
        ObjectField gameObjectField = new ObjectField("Target GameObject")
        {
            // Permettre la sélection d'objets de scène
            allowSceneObjects = true
        };

        // Trouver la propriété SerializedProperty correspondant au champ de l'objet
        SerializedProperty gameObjectProp = serializedObject.FindProperty("_targetGameObject");

        // Lier le champ ObjectField à la propriété SerializedProperty
        gameObjectField.BindProperty(gameObjectProp);

        // Ajouter le champ ObjectField à l'interface utilisateur
        Add(gameObjectField);
        Add(container);
    }

    
}
