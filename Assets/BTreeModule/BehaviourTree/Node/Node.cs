using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor.Experimental.GraphView;
#endif
using UnityEngine;
// Classe Node
// Classe de base abstraite pour les nœuds dans un arbre comportemental. 
// Chaque nœud représente une tâche ou une décision et peut être dans un état de succès, d'échec ou en cours d'exécution.
public abstract class Node : ScriptableObject
{
    // Énumération pour les états possibles d'un nœud.
    public enum State
    {
        Running,
        Success,
        Failure
    }

    public State state = State.Running;  // État actuel du nœud
    public bool started = false;  // Indique si le nœud a déjà démarré
    public string guid;  // Identifiant unique du nœud
    public Vector2 position;  // Position du nœud dans l'interface utilisateur
    [HideInInspector] public Blackboard blackboard;
    // Fonction Update
    // Met à jour l'état du nœud en exécutant la logique OnStart, OnUpdate et OnStop.
    public State Update()
    {
        if (!started)
        {
            OnStart();
            started = true;
        }
        state = OnUpdate();
        if (state != State.Running)
        {
            OnStop();
            started = false;
        }
       // Debug.Log($"Update called - Started: {started}, State: {state}");

        return state;
    }

    // Fonction Clone
    // Crée et retourne une copie de ce nœud.
    public virtual Node Clone()
    {
        return Instantiate(this);
    }

    // Fonction OnStart (Abstraite)
    // Définie dans les classes dérivées pour implémenter la logique au démarrage du nœud.
    protected abstract void OnStart();

    // Fonction OnStop (Abstraite)
    // Définie dans les classes dérivées pour implémenter la logique à l'arrêt du nœud.
    protected abstract void OnStop();

    // Fonction OnUpdate (Abstraite)
    // Définie dans les classes dérivées pour implémenter la logique de mise à jour du nœud.
    protected abstract State OnUpdate();

}
