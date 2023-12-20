using UnityEngine;

namespace Assets.Scripts.FSM.States
{
    [CreateAssetMenu(fileName ="IdleState", menuName ="Unity-FSM/States/Idle", order = 1)]
    public class IdleState : AbstractFSMState
    {
        [SerializeField] float _idleDuration = 3f;

        float _totalDuration;

        public override void OnEnable()
        {
            base.OnEnable();
            StateType = FSMStateType.IDLE;
        }

        public override bool EnterState(FiniteStateMachine context)
        {
            EnteredState = base.EnterState(context);

            if (EnteredState)
            {
                Debug.Log("ENTERED IDLE STATE");
                _totalDuration = 0f;
            }
            return EnteredState;
        }

        public override void UpdateState(FiniteStateMachine context)
        {
            if (EnteredState)
            {
                _totalDuration += Time.deltaTime;
                Debug.Log("UPDATING IDLE STATE: " + _totalDuration + " seconds");

                if (_totalDuration >= _idleDuration)
                {
                    _fsm.EnterState(FSMStateType.CHASE);
                }
            }
        }

        public override bool ExitState(FiniteStateMachine context)
        {
            base.ExitState(context);
            Debug.Log("EXIT IDLE STATE");
            return true;
        }
    }
}