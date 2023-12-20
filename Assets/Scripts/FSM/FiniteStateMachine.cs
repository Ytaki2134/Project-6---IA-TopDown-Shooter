using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.NPCCode;

namespace Assets.Scripts.FSM
{
    public class FiniteStateMachine : MonoBehaviour
    {
        AbstractFSMState _currentState;

        [SerializeField] List<AbstractFSMState> _validStates;

        Dictionary<FSMStateType, AbstractFSMState> _fsmStates;

        private Movement movement;
        public Movement Movement { get => movement; set => movement = value; }

        private GameObject player;
        public GameObject Player { get => player; set => player = value; }

        public void Awake()
        {
            _currentState = null;

            _fsmStates = new Dictionary<FSMStateType, AbstractFSMState>();

            NPC npc = this.GetComponent<NPC>();
            player = npc.GetPlayer();

            movement = GetComponentInChildren<Movement>();

            foreach (AbstractFSMState state in _validStates)
            {
                state.SetExecutingFSM(this);
                state.SetExecutingNPC(npc);
                _fsmStates.Add(state.StateType, state);
            }
        }

        public void Start()
        {
            EnterState(FSMStateType.IDLE);
        }

        public void Update()
        {
            if( _currentState != null)
            {
                _currentState.UpdateState(this);
            }
        }

        #region STATE MANAGEMENT

        public void EnterState(AbstractFSMState nextState)
        {
            if(nextState == null)
            {
                return;
            }

            if (_currentState != null)
            {
                _currentState.ExitState(this);
            }
            
            _currentState = nextState;
            _currentState.EnterState(this);
        }

        public void EnterState(FSMStateType stateType)
        {
            if (_fsmStates.ContainsKey(stateType))
            {
                AbstractFSMState nextState = _fsmStates[stateType];

                EnterState(nextState);
            }
        }

        #endregion
    }
}