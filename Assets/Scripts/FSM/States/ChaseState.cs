using Assets.Scripts.NPCCode;
using UnityEngine;

namespace Assets.Scripts.FSM
{
    [CreateAssetMenu(fileName = "ChaseState", menuName = "Unity-FSM/States/Chase", order = 3)]
    public class ChaseState : AbstractFSMState
    {
        private Movement m_movement;
        private float _fireRange;
        //private GameObject player;

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

                    m_movement.SetSpeed(3f);
                    m_movement.SetRotationSpeed(2f);
                    m_movement.SetBrakeSpeed(1f);
                    m_movement.SetBrakeRotationSpeed(1f);

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
                _fireRange = Vector2.Distance(_npc.transform.position, context.Player.transform.position);
                if (_fireRange <= 25f)
                {
                    _fsm.EnterState(FSMStateType.CHASE_AND_FIRE);
                }
                else if (_fireRange >= 100f)
                {
                    _fsm.EnterState(FSMStateType.IDLE);
                }
                else
                {
                    if (m_movement != null)
                    {
                        m_movement.SetCurrentMovement(((Vector2)context.Player.transform.position - (Vector2)_npc.transform.position).normalized);
                        m_movement.Move();
                    }
                }
            }
        }
    }
}