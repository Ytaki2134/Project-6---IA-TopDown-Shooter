using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.FSM.States
{
    [CreateAssetMenu(fileName ="DeadState", menuName ="Unity-FSM/States/Dead", order = 8)]
    public class DeadState : AbstractFSMState
    {
        private float _distance;

        public override void OnEnable()
        {
            base.OnEnable();
            StateType = FSMStateType.DEAD;
        }

        public override bool EnterState(FiniteStateMachine context)
        {
            EnteredState = base.EnterState(context);

            if (EnteredState)
            {
                Debug.Log("ENTERED DEAD STATE");
            }
            return EnteredState;
        }

        public override void UpdateState(FiniteStateMachine context)
        {
            if (EnteredState)
            {
                //Debug.Log("UPDATING DEAD STATE");
            }
        }

        public override bool ExitState(FiniteStateMachine context)
        {
            base.ExitState(context);
            Debug.Log("EXIT DEAD STATE");
            return true;
        }
    }
}