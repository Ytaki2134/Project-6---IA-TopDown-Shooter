using Assets.Scripts.NPCCode;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.FSM.States
{
    [CreateAssetMenu(fileName = "FleeAndFire", menuName = "Unity-FSM/States/FleeAndFire", order = 6)]
    public class FleeAndFireState : AbstractFSMState
    {
        private Movement m_movement;
        private float _distance;
        private bool _canFire;

        public override void OnEnable()
        {
            base.OnEnable();
            _canFire = false;
            StateType = FSMStateType.FLEE_AND_FIRE;
        }


        public override bool EnterState(FiniteStateMachine context)
        {
            EnteredState = false;
            if (base.EnterState(context))
            {
                Debug.Log("ENTERED FLEE FIRE STATE");

                if (context.Player == null)
                {
                    Debug.Log("FleeFireState: failed");
                }
                else
                {
                    m_movement = context.Movement;

                    m_movement.SetSpeed(context.TankStatistics.Speed);
                    m_movement.SetRotationSpeed(context.TankStatistics.RotationSpeed);
                    m_movement.SetBrakeSpeed(context.TankStatistics.BrakeSpeedMod);
                    m_movement.SetBrakeRotationSpeed(context.TankStatistics.BrakeRotationSpeedMod);

                    CheckWeapon(context);
                    EnteredState = true;
                }
            }
            return EnteredState;
        }

        public override void UpdateState(FiniteStateMachine context)
        {
            if (EnteredState)
            {
                if (_canFire)
                {
                    Debug.Log("UPDATING FLEE AND FIRE STATE");
                    _distance = Vector2.Distance(_npc.transform.position, context.Player.transform.position);


                    switch (context.Index)
                    {
                        case 3:
                            if (_distance >= 15f)
                            {
                                _fsm.EnterState(FSMStateType.IDLE_AND_FIRE);
                            }
                            break;

                        default:
                            context.Gun.SetTargetPosition(context.Player.transform.position);
                            context.Gun.FollowTargetPosition();
                            context.Gun.Fire();

                            if (m_movement != null)
                            {
                                m_movement.SetCurrentMovement(((Vector2)context.Player.transform.position + (Vector2)_npc.transform.position).normalized);
                                m_movement.Move();
                            }
                            break;
                    }
                }
            }
        }

        public void CheckWeapon(FiniteStateMachine context)
        {
            if (context.Gun != null)
            {
                _canFire = true;
            }
        }
    }
}
