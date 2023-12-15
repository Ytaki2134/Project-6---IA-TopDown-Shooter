using UnityEngine;

namespace Assets.Scripts.FSM.States
{
    [CreateAssetMenu(fileName ="IdleState", menuName ="Unity-FSM/States/Idle", order = 1)]
    public class IdleState : AbstractFSMState
    {
        public override bool EnterState()
        {
            base.EnterState();
            Debug.Log("ENTERED IDLE STATE");
            return true;
        }

        public override void UpdateState()
        {
            Debug.Log("UPDATING IDLE STATE");
        }

        public override bool ExitState()
        {
            base.ExitState();
            Debug.Log("EXIT IDLE STATE");
            return true;
        }
    }
}