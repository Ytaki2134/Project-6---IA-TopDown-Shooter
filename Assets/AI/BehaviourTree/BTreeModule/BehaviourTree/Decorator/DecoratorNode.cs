using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
// Classe DecoratorNode
// Cette classe abstraite sert de base pour les n�uds d�corateurs dans un arbre comportemental.
// Un n�ud d�corateur modifie le comportement de son n�ud enfant, soit en changeant son r�sultat, soit en r�p�tant son ex�cution, etc.
public abstract class DecoratorNode : Node
{
    [SerializeField]
    protected Node child;  // Le n�ud enfant que ce d�corateur va modifier ou contr�ler

    // Propri�t� m_child
    // Permet d'acc�der et de modifier le n�ud enfant du d�corateur.
    public Node m_child
    {
        get { return child; }
        set { child = value; }
    }

    // Fonction Clone
    // Cr�e et retourne une copie de ce n�ud d�corateur, y compris son enfant.
    public override Node Clone()
    {
        DecoratorNode node = Instantiate(this);
        node.m_child = child.Clone(); // Assurez-vous de cloner �galement l'enfant
        return node;
    }

    // Les m�thodes abstraites OnStart, OnStop et OnUpdate doivent �tre impl�ment�es dans les sous-classes.
}
#endif
