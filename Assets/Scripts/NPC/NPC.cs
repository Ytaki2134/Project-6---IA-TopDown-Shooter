using Assets.Scripts.FSM;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.NPC
{
    [RequireComponent(typeof(NavMeshAgent), typeof(FiniteStateMachine))]
    public class NPC : MonoBehaviour
    {
        //[SerializeField] NPCPatrolPoints _patrolPoints;

        NavMeshAgent _navMeshAgent;
        FiniteStateMachine _finiteStateMachine;

        public void Awake()
        {
            _navMeshAgent = this.GetComponent<NavMeshAgent>();
            _finiteStateMachine = this.GetComponent<FiniteStateMachine>();
        }

        public void Start()
        {

        }

        public void Update()
        {

        }

        /*public NPCPatrolPoint[] PatrolPoints
        {
            get
            {
                return _patrolPoints;
            }
        }*/
    }
}