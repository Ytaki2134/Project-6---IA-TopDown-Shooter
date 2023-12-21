using Assets.Scripts.FSM;
using UnityEngine;

namespace Assets.Scripts.NPCCode
{
    //[RequireComponent(typeof(NavMeshAgent), typeof(FiniteStateMachine))]

    public class NPC : MonoBehaviour
    {
        [SerializeField] PatrolPoints[] _patrolPoints;
        [SerializeField] GameObject _player;
        [SerializeField] int _index;
        // Index : 1 = Standard, 2 = SpreadShot, 3 = Sniper, 4 = Missile, 5 = lance-flamme

        FiniteStateMachine _finiteStateMachine;

        public void Awake()
        {
            _finiteStateMachine = this.GetComponent<FiniteStateMachine>();
            //_player = GameObject.FindGameObjectWithTag("Player");
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
            if (_player != null)
            {
                Debug.Log("Player found: " + _player);
            }
            else
            {
                Debug.LogWarning("Player not found!");
            }
            return _player;
        }

        public int GetIndex()
        {
            return _index;
        }
    }
}
