using Assets.Scripts.NPCCode;
using UnityEngine;

namespace Assets.Scripts.FSM
{
    [CreateAssetMenu(fileName = "ChaseState", menuName = "Unity-FSM/States/Chase", order = 3)]
    public class ChaseState : AbstractFSMState
    {
        private Movement m_movement;
        private float _distance;

        public override void OnEnable()
        {
            base.OnEnable();
            StateType = FSMStateType.CHASE;
        }

        public override bool EnterState(FiniteStateMachine context)
        {
            EnteredState = false;
            if (base.EnterState(context))
            {
                Debug.Log("ENTERED CHASE STATE");

                if (context.Player == null)
                {
                    Debug.Log("ChaseState: failed to grab player");
                }
                else
                {
                    m_movement = context.Movement;

                    m_movement.SetSpeed(context.TankStatistics.Speed);
                    m_movement.SetRotationSpeed(context.TankStatistics.RotationSpeed);
                    m_movement.SetBrakeSpeed(context.TankStatistics.BrakeSpeedMod);
                    m_movement.SetBrakeRotationSpeed(context.TankStatistics.BrakeRotationSpeedMod);

                    EnteredState = true;
                }
            }

            return EnteredState;
        }

        public override void UpdateState(FiniteStateMachine context)
        {
            if (EnteredState)
            {
                Debug.Log("UPDATING CHASE STATE");
                _distance = Vector2.Distance(_npc.transform.position, context.Player.transform.position);

                switch (context.Index)
                {
                    case 1:
                        if (_distance <= 10f)
                        {
                            _fsm.EnterState(FSMStateType.CHASE_AND_FIRE);
                        }
                        else if (_distance >= 20f)
                        {
                            _fsm.EnterState(FSMStateType.PATROL);
                        }
                        break;

                    case 2:
                        if (_distance <= 6f)
                        {
                            _fsm.EnterState(FSMStateType.CHASE_AND_FIRE);
                        }
                        else if (_distance >= 20f)
                        {
                            _fsm.EnterState(FSMStateType.PATROL);
                        }
                        break;

                    case 3:
                        break;

                    case 4:
                        if (_distance >= 25f)
                        {
                            _fsm.EnterState(FSMStateType.IDLE);
                        }
                        break;

                    case 5:
                        if (_distance <= 4f)
                        {
                            _fsm.EnterState(FSMStateType.CHASE_AND_FIRE);
                        }
                        break;

                    default:
                        if (m_movement != null)
                        {
                            m_movement.SetCurrentMovement(((Vector2)context.Player.transform.position - (Vector2)_npc.transform.position).normalized);
                            m_movement.Move();
                        }
                        break;
                }
            }
        }
    }
}