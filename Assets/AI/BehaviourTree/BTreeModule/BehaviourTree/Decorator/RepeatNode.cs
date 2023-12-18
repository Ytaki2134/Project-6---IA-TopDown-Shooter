using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
// Classe RepeatNode
// Cette classe est une spécialisation de DecoratorNode et représente un nœud décorateur qui répète continuellement l'exécution de son nœud enfant.
// Le nœud RepeatNode continue de renvoyer l'état Running, ce qui entraîne une répétition constante du comportement de l'enfant.
public class RepeatNode : DecoratorNode
{

    // Fonction OnStart
    // Implémentée si une logique spéciale est nécessaire au démarrage du nœud RepeatNode.
    protected override void OnStart()
    {
    }

    // Fonction OnStop
    // Implémentée si une logique spéciale est nécessaire à l'arrêt du nœud RepeatNode.
    protected override void OnStop()
    {
    }

    // Fonction OnUpdate
    // À chaque mise à jour, exécute la mise à jour du nœud enfant et retourne toujours l'état Running.
    // Cela crée un cycle de répétition continuelle pour le nœud enfant.
    protected override State OnUpdate()
    {

        m_child.Update();
        return State.Running;
    }
}
#endif