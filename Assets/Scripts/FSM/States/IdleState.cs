using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.FSM.States
{
    [CreateAssetMenu(fileName ="IdleState", menuName ="Unity-FSM/States/Idle", order = 1)]
    public class IdleState : AbstractFSMState
    {
        private float _distance;

        public override void OnEnable()
        {
            base.OnEnable();
            StateType = FSMStateType.IDLE;
        }

        public override bool EnterState(FiniteStateMachine context)
        {
            EnteredState = base.EnterState(context);

            if (EnteredState)
            {
                Debug.Log("ENTERED IDLE STATE");
            }
            return EnteredState;
        }

        public override void UpdateState(FiniteStateMachine context)
        {
            if (EnteredState)
            {
                Debug.Log("UPDATING IDLE STATE");
                _distance = Vector2.Distance(_npc.transform.position, context.Player.transform.position);
                
                if (context.TankStatistics.Health <= 0 )
                {
                    _fsm.EnterState(FSMStateType.DEAD);
                }
                else
                {
                    switch (context.Index)
                    {
                        case 1:
                        case 2:
                        case 5:
                            if (_distance <= 15f)
                            {
                                _fsm.EnterState(FSMStateType.PATROL);
                                break;
                            }
                            else
                            {
                                _fsm.EnterState(FSMStateType.PATROL);
                                break;
                            }

                        case 3:
                            if (_distance <= 30f)
                            {
                                _fsm.EnterState(FSMStateType.IDLE_AND_FIRE);
                            }
                            break;

                        case 4:
                            if (_distance <= 20f)
                            {
                                _fsm.EnterState(FSMStateType.CHASE_AND_FIRE);
                            }
                            break;
                    }
                }
            }
        }

        public override bool ExitState(FiniteStateMachine context)
        {
            base.ExitState(context);
            Debug.Log("EXIT IDLE STATE");
            return true;
        }
    }
}