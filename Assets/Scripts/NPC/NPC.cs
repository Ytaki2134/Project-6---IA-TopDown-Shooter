using Assets.Scripts.FSM;
using UnityEngine;

namespace Assets.Scripts.NPCCode
{
    //[RequireComponent(typeof(NavMeshAgent), typeof(FiniteStateMachine))]

    public class NPC : MonoBehaviour
    {
        [SerializeField] PatrolPoints[] _patrolPoints;
         private GameObject _player;

        FiniteStateMachine _finiteStateMachine;

        public void Awake()
        {
            _finiteStateMachine = this.GetComponent<FiniteStateMachine>();
            _player = GameObject.FindGameObjectWithTag("Player");
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

        public GameObject GetPlayer()
        {
            return _player;
        }
    }
}
