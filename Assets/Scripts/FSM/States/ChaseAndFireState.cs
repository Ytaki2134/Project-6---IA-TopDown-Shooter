using Assets.Scripts.NPCCode;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.Scripts.FSM.States
{
    [CreateAssetMenu(fileName = "ChaseAndFire", menuName = "Unity-FSM/States/ChaseAndFire", order = 4)]
    public class ChaseAndFireState : AbstractFSMState
    {
        bool _canFire;
        float _distance;
        private Gun m_gun;

        public override void OnEnable()
        {
            base.OnEnable();
            _canFire = false;
            StateType = FSMStateType.CHASE_AND_FIRE;
        }


        public override bool EnterState(FiniteStateMachine context)
        {
            if (context.Player != null)
            {
                _distance = Vector2.Distance(_npc.transform.position, context.Player.transform.position);
            }

            EnteredState = false;
            if (base.EnterState(context))
            {
                Debug.Log("ENTERED CHASE FIRE STATE");

                if (_canFire == false || _distance <= 1.5f)
                {
                    Debug.Log("ChaseFireState: failed");
                    CheckWeapon();
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
                if (_canFire)
                {
                    Debug.Log("UPDATING IDLE STATE");

                    if (_distance <= 1.5f)
                    {
                        m_gun.SetTargetPosition(context.Player.transform.position);
                        m_gun.FollowTargetPosition();
                        m_gun.Fire();
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
