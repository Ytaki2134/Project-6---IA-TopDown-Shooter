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
            _distance = Vector2.Distance(_npc.transform.position, .transform.position);
            StateType = FSMStateType.CHASE_AND_FIRE;
        }

        public override bool EnterState()
        {
            EnteredState = false;   
            if (base.EnterState())
            {
                Debug.Log("ENTERED CHASE FIRE STATE");

                if (_canFire == true || _distance <=1.5f)
                {
                    Debug.Log("ChaseFireState: failed");
                    CheckWeapon();
                }
            }
            return base.EnterState();
        }

        public override void UpdateState()
        {
            if (EnteredState)
            {
                if (_canFire)
                {
                    Debug.Log("UPDATING IDLE STATE");

                    if (_distance <= 1.5f)
                    {
                        m_gun.SetTargetPosition();
                        m_gun.FollowTargetPosition();
                        m_gun.Fire();
                    }
                }
            }
        }

        public void CheckWeapon()
        {
            if(m_gun != null)
            {
                _canFire = true;
            }
        } 
    }
}