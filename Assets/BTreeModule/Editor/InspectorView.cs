using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
// Classe InspectorView
// Cette classe est un �l�ment d'interface utilisateur personnalis� pour Unity qui agit comme un inspecteur.
// Elle est utilis�e pour afficher et modifier les propri�t�s de l'objet s�lectionn�, similaire � l'inspecteur standard de Unity.
public class InspectorView : VisualElement
{
    // Factory pour la cr�ation de l'�l�ment InspectorView dans l'interface utilisateur UXML
    public new class UxmlFactory : UxmlFactory<InspectorView, VisualElement.UxmlTraits> { }

    // L'�diteur associ� � l'objet s�lectionn�
    Editor editor;


    // Constructeur par d�faut
    public InspectorView()
    {

    }
    public void UpdateWithSerializedObject(SerializedObject serializedObject)
    {
        Clear();

        // It�rer sur les propri�t�s en utilisant la propri�t� racine
        SerializedProperty iterator = serializedObject.GetIterator();
        bool enterChildren = true;
        while (iterator.NextVisible(enterChildren))
        {
            // La premi�re fois, nous voulons entrer dans les enfants de la premi�re propri�t�
            enterChildren = false;

            // Cr�ez un PropertyField pour chaque propri�t� trouv�e
            PropertyField propertyField = new PropertyField(iterator.Copy()) { name = "PropertyField:" + iterator.propertyPath };

            if (iterator.propertyPath == "m_Script" && serializedObject.targetObject != null)
            {
                propertyField.SetEnabled(value: false); // D�sactive le champ du script pour qu'il ne soit pas modifiable
            }
            else
            {
                Add(propertyField);
                propertyField.Bind(serializedObject); // Lie la propri�t� SerializedProperty au SerializedObject
            }
        }
        serializedObject.ApplyModifiedProperties();
    }

    // M�thode UpdateSelection
    // Met � jour l'inspecteur pour afficher les propri�t�s de l'objet NodeView s�lectionn�.
    internal void UpdateSelection(NodeView nodeView)
    {
        // Efface les �l�ments actuels de l'inspecteur
        Clear();

        // D�truit l'�diteur existant pour �viter les fuites de m�moire
        UnityEngine.Object.DestroyImmediate(editor);

        // Cr�e un nouvel �diteur pour l'objet s�lectionn�
        editor = Editor.CreateEditor(nodeView.node);

        // Cr�e et ajoute un conteneur IMGUI pour afficher l'interface de l'�diteur
        IMGUIContainer container = new IMGUIContainer(() => {
            if (editor.target)
            {
                editor.OnInspectorGUI(); 
            }
        });

        // Cr�ation d'un SerializedObject � partir de l'objet cible (nodeView.node)
        SerializedObject serializedObject = new SerializedObject(nodeView.node);

        // Cr�ation d'un ObjectField pour la propri�t� que vous voulez modifier
        ObjectField gameObjectField = new ObjectField("Target GameObject")
        {
            // Permettre la s�lection d'objets de sc�ne
            allowSceneObjects = true
        };

        // Trouver la propri�t� SerializedProperty correspondant au champ de l'objet
        SerializedProperty gameObjectProp = serializedObject.FindProperty("_targetGameObject");

        // Lier le champ ObjectField � la propri�t� SerializedProperty
        gameObjectField.BindProperty(gameObjectProp);

        // Ajouter le champ ObjectField � l'interface utilisateur
        Add(gameObjectField);
        Add(container);
    }

    
}
