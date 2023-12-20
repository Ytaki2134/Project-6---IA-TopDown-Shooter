using Assets.Scripts.NPCCode;
using UnityEngine;

namespace Assets.Scripts.FSM.States
{
    [CreateAssetMenu(fileName = "FleeAndFire", menuName = "Unity-FSM/States/FleeAndFire", order = 6)]
    public class FleeAndFireState : AbstractFSMState
    {
        private Movement m_movement;
        private float _fireRange;
        private bool _canFire;
        private Gun m_gun;

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
                    Debug.Log("UPDATING FLEE STATE");
                    _fireRange = Vector2.Distance(_npc.transform.position, context.Player.transform.position);
                    if (_fireRange > 1.5f)
                    {
                        _fsm.EnterState(FSMStateType.FLEE);
                    }
                    else
                    {
                        m_gun.SetTargetPosition(context.Player.transform.position);
                        m_gun.FollowTargetPosition();
                        m_gun.Fire();

                        if (m_movement != null)
                        {
                            m_movement.SetCurrentMovement(((Vector2)context.Player.transform.position + (Vector2)_npc.transform.position).normalized);
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
