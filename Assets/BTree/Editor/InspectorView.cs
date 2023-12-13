using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
        // Initialisation de l'InspectorView (optionnellement, ajoutez ici la logique d'initialisation)
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
        IMGUIContainer container = new IMGUIContainer(() => { editor.OnInspectorGUI(); });
        Add(container);
    }
}
