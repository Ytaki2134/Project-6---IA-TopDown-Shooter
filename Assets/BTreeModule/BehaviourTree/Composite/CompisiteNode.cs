using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using static Unity.VisualScripting.Metadata;
#endif
// Classe CompositeNode
// Cette classe abstraite est une extension de la classe Node et repr�sente un n�ud composite dans un arbre comportemental.
// Un n�ud composite peut avoir plusieurs n�uds enfants et est utilis� pour construire des structures de contr�le complexes dans l'arbre.
public abstract class CompisiteNode : Node
{
    [SerializeField]
    protected List<Node> children = new List<Node>();  // Liste des n�uds enfants de ce n�ud composite

    // M�thode AddChildren
    // Ajoute un n�ud � la liste des enfants de ce n�ud composite.
    public void AddChildren(Node node) { children.Add(node); }

    // M�thode RemoveChildren
    // Supprime un n�ud de la liste des enfants de ce n�ud composite.
    public void RemoveChildren(Node node) { children.Remove(node); }

    // Propri�t� m_children
    // Permet d'acc�der et de modifier la liste des n�uds enfants du n�ud composite.
    public List<Node> m_children
    {
        get { return children; }
        set { children = value; }
    }

    // M�thode Clone
    // Cr�e et retourne une copie de ce n�ud composite, y compris une copie de tous ses enfants.
    public override Node Clone()
    {
        CompisiteNode node = Instantiate(this);
        node.m_children = children.ConvertAll(c => c.Clone());  // Cloner chaque enfant
        return node;
    }
}
