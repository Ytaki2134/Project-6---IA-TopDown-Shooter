using Assets.Scripts.NPCCode;
using UnityEngine;

namespace Assets.Scripts.FSM.States
{
    [CreateAssetMenu(fileName = "ChaseAndFire", menuName = "Unity-FSM/States/ChaseAndFire", order = 5)]
    public class ChaseAndFireState : AbstractFSMState
    {
        private Movement m_movement;
        private float _distance;
        private bool _canFire;

        public override void OnEnable()
        {
            base.OnEnable();
            _canFire = false;
            StateType = FSMStateType.CHASE_AND_FIRE;
        }


        public override bool EnterState(FiniteStateMachine context)
        {
            EnteredState = false;
            if (base.EnterState(context))
            {
                Debug.Log("ENTERED CHASE FIRE STATE");

                if (context.Player == null)
                {
                    Debug.Log("ChaseFireState: failed");
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
                    //Debug.Log("UPDATING CHASE AND FIRE STATE");
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
                            case 2:
                                if (_distance >= 15f)
                                {
                                    _fsm.EnterState(FSMStateType.CHASE);
                                }
                                break;

                            case 3:
                                break;

                            case 4:
                                if (_distance >= 20f)
                                {
                                    _fsm.EnterState(FSMStateType.CHASE);
                                }
                                else if (_distance <= 15f)
                                {
                                    _fsm.EnterState(FSMStateType.FLEE_AND_FIRE);
                                }
                                break;

                            case 5:
                                if (_distance >= 20f)
                                {
                                    _fsm.EnterState(FSMStateType.CHASE);
                                }
                                break;
                        }
                        context.Gun.SetTargetPosition(context.Player.transform.position);
                        context.Gun.Fire();

                        if (m_movement != null)
                        {
                            m_movement.SetCurrentMovement(((Vector2)context.Player.transform.position - (Vector2)_npc.transform.position).normalized);
                            m_movement.Move();
                        }
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
