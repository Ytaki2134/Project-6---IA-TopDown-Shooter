using UnityEngine;
using FSM;
using UnityEngine.AI;

[RequireComponent (typeof(Animator), typeof(NavMeshAgent))]

public class Enemy : MonoBehaviour
{
    private StateMachine<EnnemyState, StateEvent> EnemyFSM;
    private Animator Animator;
    private NavMeshAgent Agent;

    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        Animator = GetComponent<Animator>();
        EnemyFSM = new StateMachine<EnemyState, StateEvent>();



    }
}
