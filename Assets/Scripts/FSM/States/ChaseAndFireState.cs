using Assets.Scripts.NPCCode;
using UnityEngine;

namespace Assets.Scripts.FSM.States
{
    [CreateAssetMenu(fileName = "ChaseAndFire", menuName = "Unity-FSM/States/ChaseAndFire", order = 5)]
    public class ChaseAndFireState : AbstractFSMState
    {
        private Movement m_movement;
        private float _fireRange;
        private bool _canFire;
        private Gun m_gun;

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

                    m_movement.SetSpeed(3f);
                    m_movement.SetRotationSpeed(2f);
                    m_movement.SetBrakeSpeed(1f);
                    m_movement.SetBrakeRotationSpeed(1f);

                    CheckWeapon();
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
                    Debug.Log("UPDATING CHASE AND FIRE STATE");
                    _fireRange = Vector2.Distance(_npc.transform.position, context.Player.transform.position);
                    if (_fireRange > 1.5f)
                    {
                        _fsm.EnterState(FSMStateType.CHASE);
                    }
                    else
                    {
                        m_gun.SetTargetPosition(context.Player.transform.position);
                        m_gun.FollowTargetPosition();
                        m_gun.Fire();

                        if (m_movement != null)
                        {
                            m_movement.SetCurrentMovement(((Vector2)context.Player.transform.position - (Vector2)_npc.transform.position).normalized);
                            m_movement.Move();
                        }
                    }
                }
            }
        }

        public void CheckWeapon()
        {
            if (m_gun != null)
            {
                _canFire = true;
            }
        }
    }
}
