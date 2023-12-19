using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Classe RootNode
// Cette classe est une sp�cialisation de Node et repr�sente le n�ud racine d'un arbre comportemental. 
// Le n�ud racine est le point de d�part de l'ex�cution de l'arbre comportemental.
public class RootNode : Node
{
    [SerializeField]
    protected Node child;  // Le n�ud enfant du n�ud racine

    // Propri�t� m_child
    // Permet d'acc�der et de modifier le n�ud enfant du n�ud racine.
    public Node m_child { get { return child; } set { child = value; } }

    // Fonction Clone
    // Cr�e et retourne une copie de ce n�ud racine, y compris son enfant.
    public override Node Clone()
    {
        
        RootNode node = Instantiate(this);
        node.m_child = child.Clone(); // Assurez-vous de cloner �galement l'enfant
        return node;
    }

    // Fonction OnStart (impl�mentation vide)
    // Impl�ment�e si une logique sp�ciale est n�cessaire au d�marrage du n�ud racine.
    protected override void OnStart()
    {

    }

    // Fonction OnStop (impl�mentation vide)
    // Impl�ment�e si une logique sp�ciale est n�cessaire � l'arr�t du n�ud racine.
    protected override void OnStop()
    {
    }

    // Fonction OnUpdate
    // Met � jour le n�ud enfant et retourne son �tat comme l'�tat du n�ud racine.
    protected override State OnUpdate()
    {

        return child.Update();
    }
}
