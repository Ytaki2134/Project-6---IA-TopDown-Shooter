﻿using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.NPCCode;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace Assets.Scripts.FSM
{
    public class FiniteStateMachine : MonoBehaviour
    {
        AbstractFSMState _currentState;

        [SerializeField] List<AbstractFSMState> _validStates;

        Dictionary<FSMStateType, AbstractFSMState> _fsmStates;

        private GameObject player;
        public GameObject Player { get => player; set => player = value; }

        private Movement movement;
        public Movement Movement { get => movement; set => movement = value; }

        private Gun gun;
        public Gun Gun { get => gun; set => gun = value; }

        private TankStatistics tankStatistics;
        public TankStatistics TankStatistics { get => tankStatistics; set => tankStatistics = value; }

        private int index;
        public int Index { get => index; set => index = value; }

        public void Awake()
        {
            _currentState = null;

            _fsmStates = new Dictionary<FSMStateType, AbstractFSMState>();

            NPC npc = this.GetComponent<NPC>();
            player = npc.GetPlayer();

            if (player != null)
            {
                Debug.Log("Player found in : " + player);
            }
            else
            {
                Debug.LogWarning("Player not found in!");
            }

            movement = GetComponentInChildren<Movement>();
            gun = GetComponentInChildren<Gun>();
            tankStatistics = GetComponent<TankStatistics>();
            index = npc.GetIndex();

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
            if (_currentState != null)
            {
                _currentState.UpdateState(this);
            }
        }

        //public void FixedUpdate()
        //{
        //    if (_currentState != null)
        //    {
        //        _currentState.UpdateState(this);
        //    }
        //}

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