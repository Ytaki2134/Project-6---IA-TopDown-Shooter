using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Classe CompositeNode
// Cette classe abstraite est une extension de la classe Node et représente un nœud composite dans un arbre comportemental.
// Un nœud composite peut avoir plusieurs nœuds enfants et est utilisé pour construire des structures de contrôle complexes dans l'arbre.
public abstract class CompisiteNode : Node
{
    [SerializeField]
    protected List<Node> children = new List<Node>();  // Liste des nœuds enfants de ce nœud composite

    // Méthode AddChildren
    // Ajoute un nœud à la liste des enfants de ce nœud composite.
    public void AddChildren(Node node) { children.Add(node); }

    // Méthode RemoveChildren
    // Supprime un nœud de la liste des enfants de ce nœud composite.
    public void RemoveChildren(Node node) { children.Remove(node); }

    // Propriété m_children
    // Permet d'accéder et de modifier la liste des nœuds enfants du nœud composite.
    public List<Node> m_children
    {
        get { return children; }
        set { children = value; }
    }

    // Méthode Clone
    // Crée et retourne une copie de ce nœud composite, y compris une copie de tous ses enfants.
    public override Node Clone()
    {
        CompisiteNode node = Instantiate(this);
        node.m_children = children.ConvertAll(c => c.Clone());  // Cloner chaque enfant
        return node;
    }
}
