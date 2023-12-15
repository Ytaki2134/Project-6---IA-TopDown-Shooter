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

        Add(container);
    }

    
}
