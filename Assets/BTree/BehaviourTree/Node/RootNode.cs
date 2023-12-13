using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Classe RootNode
// Cette classe est une spécialisation de Node et représente le nœud racine d'un arbre comportemental. 
// Le nœud racine est le point de départ de l'exécution de l'arbre comportemental.
public class RootNode : Node
{
    private Node child;  // Le nœud enfant du nœud racine

    // Propriété m_child
    // Permet d'accéder et de modifier le nœud enfant du nœud racine.
    public Node m_child { get { return child; } set { child = value; } }

    // Fonction Clone
    // Crée et retourne une copie de ce nœud racine, y compris son enfant.
    public override Node Clone()
    {
        RootNode node = Instantiate(this);
        node.m_child = child.Clone(); // Assurez-vous de cloner également l'enfant
        return node;
    }

    // Fonction OnStart (implémentation vide)
    // Implémentée si une logique spéciale est nécessaire au démarrage du nœud racine.
    protected override void OnStart()
    {
    }

    // Fonction OnStop (implémentation vide)
    // Implémentée si une logique spéciale est nécessaire à l'arrêt du nœud racine.
    protected override void OnStop()
    {
    }

    // Fonction OnUpdate
    // Met à jour le nœud enfant et retourne son état comme l'état du nœud racine.
    protected override State OnUpdate()
    {
        return child.Update();
    }
}
