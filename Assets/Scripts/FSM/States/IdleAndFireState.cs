using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.FSM.States
{
    [CreateAssetMenu(fileName = "IdleAndFireState", menuName = "Unity-FSM/States/IdleAndFireState", order = 7)]
    public class IdleAndFireState : AbstractFSMState
    {
        private float _distance;

        public override void OnEnable()
        {
            base.OnEnable();
            StateType = FSMStateType.IDLE_AND_FIRE;
        }

        public override bool EnterState(FiniteStateMachine context)
        {
            EnteredState = base.EnterState(context);

            if (EnteredState)
            {
                Debug.Log("ENTERED IDLE AND FIRE STATE");
            }
            return EnteredState;
        }

        public override void UpdateState(FiniteStateMachine context)
        {
            if (EnteredState)
            {
                Debug.Log("UPDATING IDLE AND FIRE STATE");
                _distance = Vector2.Distance(_npc.transform.position, context.Player.transform.position);

                if (context.TankStatistics.Health <= 0)
                {
                    _fsm.EnterState(FSMStateType.DEAD);
                }
                else
                {
                    switch (context.Index)
                    {
                        case 1:
                            break;

                        case 2:
                            break;

                        case 3:
                            if (_distance <= 15f)
                            {
                                _fsm.EnterState(FSMStateType.FLEE_AND_FIRE);
                            }
                            else if (_distance >= 30f)
                            {
                                _fsm.EnterState(FSMStateType.IDLE);
                            }
                            break;

                        case 4:
                            if (_distance >= 15f)
                            {
                                _fsm.EnterState(FSMStateType.CHASE_AND_FIRE);
                            }
                            break;
                    }
                    context.Gun.SetTargetPosition(context.Player.transform.position);
                    context.Gun.Fire();
                }
            }
        }

        public override bool ExitState(FiniteStateMachine context)
        {
            base.ExitState(context);
            Debug.Log("EXIT IDLE AND FIRE STATE");
            return true;
        }
    }
}