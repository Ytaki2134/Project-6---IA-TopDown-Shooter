using Assets.Scripts.NPCCode;
using UnityEngine;

namespace Assets.Scripts.FSM
{
    [CreateAssetMenu(fileName = "FleeState", menuName = "Unity-FSM/States/Flee", order = 4)]
    public class FleeState : AbstractFSMState
    {
        private Movement m_movement;
        private float _fireRange;
        //private GameObject player;

        public override void OnEnable()
        {
            base.OnEnable();
            StateType = FSMStateType.FLEE;
        }

        public override bool EnterState(FiniteStateMachine context)
        {
            EnteredState = false;
            if (base.EnterState(context))
            {
                Debug.Log("ENTERED FLEE STATE");

                if (context.Player == null)
                {
                    Debug.Log("FleeState: failed to grab player");
                }
                else
                {
                    EnteredState = true;
                }
            }

            return EnteredState;
        }

        public override void UpdateState(FiniteStateMachine context)
        {
            if (EnteredState)
            {
                //Debug.Log("UPDATING CHASE STATE");
                _fireRange = Vector2.Distance(_npc.transform.position, context.Player.transform.position);
                if (_fireRange <= 1.5f)
                {
                    _fsm.EnterState(FSMStateType.FLEE_AND_FIRE);
                }
                else if (_fireRange >= 5f)
                {
                    _fsm.EnterState(FSMStateType.IDLE);
                }
                else
                {
                    if (m_movement != null)
                    {
                        m_movement.SetCurrentMovement(((Vector2)context.Player.transform.position + (Vector2)_npc.transform.position).normalized);
                        m_movement.Move();
                    }
                }
            }
        }
    }
}