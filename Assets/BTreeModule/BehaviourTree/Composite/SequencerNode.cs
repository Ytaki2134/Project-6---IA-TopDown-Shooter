using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
// Classe SequencerNode
// Cette classe est une spécialisation de CompisiteNode et représente un nœud séquenceur dans un arbre comportemental.
// Un SequencerNode exécute ses nœuds enfants séquentiellement jusqu'à ce que l'un d'eux échoue ou que tous réussissent.
public class SequencerNode : CompisiteNode
{
    int current;  // Index du nœud enfant actuellement en cours d'exécution

    // Fonction OnStart
    // Initialisée à chaque début d'exécution du nœud, réinitialise l'index du nœud enfant actuel à 0.
    protected override void OnStart()
    {
        current = 0;
    }

    // Fonction OnStop (implémentation vide)
    // Implémentée si une logique spéciale est nécessaire à l'arrêt du nœud séquenceur.
    protected override void OnStop()
    {
    }

    // Fonction OnUpdate
    // Met à jour le nœud séquenceur en exécutant ses nœuds enfants séquentiellement.
    // Retourne l'état Running tant que les nœuds enfants sont en cours d'exécution, Success si tous les enfants ont réussi, ou Failure si l'un d'eux échoue.
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
    // Retourne la liste des nœuds enfants du nœud séquenceur.
    public List<Node> GetChildren() { return children; }
}

#endif