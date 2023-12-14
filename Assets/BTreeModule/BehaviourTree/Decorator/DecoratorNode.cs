using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Classe DecoratorNode
// Cette classe abstraite sert de base pour les nœuds décorateurs dans un arbre comportemental.
// Un nœud décorateur modifie le comportement de son nœud enfant, soit en changeant son résultat, soit en répétant son exécution, etc.
public abstract class DecoratorNode : Node
{
    [SerializeField]
    protected Node child;  // Le nœud enfant que ce décorateur va modifier ou contrôler

    // Propriété m_child
    // Permet d'accéder et de modifier le nœud enfant du décorateur.
    public Node m_child
    {
        get { return child; }
        set { child = value; }
    }

    // Fonction Clone
    // Crée et retourne une copie de ce nœud décorateur, y compris son enfant.
    public override Node Clone()
    {
        DecoratorNode node = Instantiate(this);
        node.m_child = child.Clone(); // Assurez-vous de cloner également l'enfant
        return node;
    }

    // Les méthodes abstraites OnStart, OnStop et OnUpdate doivent être implémentées dans les sous-classes.
}
