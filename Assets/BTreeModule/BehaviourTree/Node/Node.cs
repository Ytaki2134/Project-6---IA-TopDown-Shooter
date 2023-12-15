using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
// Classe Node
// Classe de base abstraite pour les n�uds dans un arbre comportemental. 
// Chaque n�ud repr�sente une t�che ou une d�cision et peut �tre dans un �tat de succ�s, d'�chec ou en cours d'ex�cution.
public abstract class Node : ScriptableObject
{
    // �num�ration pour les �tats possibles d'un n�ud.
    public enum State
    {
        Running,
        Success,
        Failure
    }

    public State state = State.Running;  // �tat actuel du n�ud
    public bool started = false;  // Indique si le n�ud a d�j� d�marr�
    public string guid;  // Identifiant unique du n�ud
    public Vector2 position;  // Position du n�ud dans l'interface utilisateur
    [HideInInspector] public Blackboard blackboard;
    // Fonction Update
    // Met � jour l'�tat du n�ud en ex�cutant la logique OnStart, OnUpdate et OnStop.
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
    // Cr�e et retourne une copie de ce n�ud.
    public virtual Node Clone()
    {
        return Instantiate(this);
    }

    // Fonction OnStart (Abstraite)
    // D�finie dans les classes d�riv�es pour impl�menter la logique au d�marrage du n�ud.
    protected abstract void OnStart();

    // Fonction OnStop (Abstraite)
    // D�finie dans les classes d�riv�es pour impl�menter la logique � l'arr�t du n�ud.
    protected abstract void OnStop();

    // Fonction OnUpdate (Abstraite)
    // D�finie dans les classes d�riv�es pour impl�menter la logique de mise � jour du n�ud.
    protected abstract State OnUpdate();

}
