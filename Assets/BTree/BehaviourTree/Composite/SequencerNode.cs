using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Classe SequencerNode
// Cette classe est une sp�cialisation de CompisiteNode et repr�sente un n�ud s�quenceur dans un arbre comportemental.
// Un SequencerNode ex�cute ses n�uds enfants s�quentiellement jusqu'� ce que l'un d'eux �choue ou que tous r�ussissent.
public class SequencerNode : CompisiteNode
{
    int current;  // Index du n�ud enfant actuellement en cours d'ex�cution

    // Fonction OnStart
    // Initialis�e � chaque d�but d'ex�cution du n�ud, r�initialise l'index du n�ud enfant actuel � 0.
    protected override void OnStart()
    {
        current = 0;
    }

    // Fonction OnStop (impl�mentation vide)
    // Impl�ment�e si une logique sp�ciale est n�cessaire � l'arr�t du n�ud s�quenceur.
    protected override void OnStop()
    {
    }

    // Fonction OnUpdate
    // Met � jour le n�ud s�quenceur en ex�cutant ses n�uds enfants s�quentiellement.
    // Retourne l'�tat Running tant que les n�uds enfants sont en cours d'ex�cution, Success si tous les enfants ont r�ussi, ou Failure si l'un d'eux �choue.
    protected override State OnUpdate()
    {
        Node child = children[current];
        switch (child.Update())
        {
            case State.Running:
                return State.Running;
            case State.Failure:
                return State.Failure;
            case State.Success:
                current++;
                break;
        }
        return current == children.Count ? State.Success : State.Running;
    }

    // Fonction GetChildren
    // Retourne la liste des n�uds enfants du n�ud s�quenceur.
    public List<Node> GetChildren() { return children; }
}

