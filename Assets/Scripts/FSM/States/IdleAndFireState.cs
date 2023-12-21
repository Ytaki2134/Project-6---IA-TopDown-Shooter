using UnityEngine;

namespace Assets.Scripts.FSM.States
{
    [CreateAssetMenu(fileName ="IdleAndFireState", menuName = "Unity-FSM/States/IdleAndFire", order = 7)]
    public class IdleAndFireState : AbstractFSMState
    {
        [SerializeField] float _idleDuration = 3f;

        float _totalDuration;

        public override void OnEnable()
        {
            base.OnEnable();
            StateType = FSMStateType.IDLE_AND_FIRE;
        }

        public override bool EnterState(FiniteStateMachine context)
        {
            EnteredState = base.EnterState(context);

            if (EnteredState)
            {
                Debug.Log("ENTERED IDLE AND FIRE STATE");
                _totalDuration = 0f;
            }
            return EnteredState;
        }

        public override void UpdateState(FiniteStateMachine context)
        {
            if (EnteredState)
            {
                _totalDuration += Time.deltaTime;
                Debug.Log("UPDATING IDLE AND FIRE STATE: " + _totalDuration + " seconds");

                if (_totalDuration >= _idleDuration)
                {
                    _fsm.EnterState(FSMStateType.IDLE);
                }
            }
        }

        public override bool ExitState(FiniteStateMachine context)
        {
            base.ExitState(context);
            Debug.Log("EXIT IDLE AND FIRE STATE");
            return true;
        }
    }
}