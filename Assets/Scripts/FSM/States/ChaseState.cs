using Assets.Scripts.NPCCode;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;


namespace Assets.Scripts.FSM
{
    [CreateAssetMenu(fileName = "ChaseState", menuName = "Unity-FSM/States/Chase", order = 3)]
    public class ChaseState : AbstractFSMState
    {

        private Movement m_movement;

        public override void OnEnable()
        {
            base.OnEnable();
            StateType = FSMStateType.CHASE;
        }

        public override bool EnterState()
        {
            EnteredState = false;
            if (base.EnterState())
            {
                Debug.Log("ENTERED CHASE STATE");


                if ( == 0)
                {
                    Debug.Log("ChaseState: failed to grab ??? from the npc");
                }
                else
                {
                    EnteredState = true;
                }
            }

            return EnteredState;
        }


        public override void UpdateState()
        {
            if (EnteredState)
            {
                //Debug.Log("UPDATING CHASE STATE");
                float distance = Vector2.Distance(_npc.transform.position, /* position du player */.transform.position);
                if (distance <= 1.5f)
                {
                    _fsm.EnterState(FSMStateType.CHASE_AND_FIRE);
                }
                else if (distance >= 5f)
                {
                    _fsm.EnterState(FSMStateType.PATROL);
                }
                else
                {
                    if (m_movement != null)
                    {
                        m_movement.SetCurrentMovement(((Vector2)/* position du player */.transform.position - (Vector2)_npc.transform.position).normalized);
                        m_movement.Move();
                    }
                }
            }
        }
    }
}