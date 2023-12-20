using Assets.Scripts.FSM;
using UnityEngine;

namespace Assets.Scripts.NPCCode
{
    //[RequireComponent(typeof(NavMeshAgent), typeof(FiniteStateMachine))]

    public class NPC : MonoBehaviour
    {
        [SerializeField] PatrolPoints[] _patrolPoints;

        FiniteStateMachine _finiteStateMachine;

        public void Awake()
        {
            _finiteStateMachine = this.GetComponent<FiniteStateMachine>();
        }

        public void Start()
        {

        }

        public void Update()
        {

        }

        public PatrolPoints[] PatrolPoints
        {
            get
            {
                return _patrolPoints;
            }
        }
    }
}
