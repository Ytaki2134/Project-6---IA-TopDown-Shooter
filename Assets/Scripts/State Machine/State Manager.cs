using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateManager<EState> : MonoBehaviour where EState : Enum
{
    protected Dictionary<EState, BaseState<EState>> States = new Dictionary<EState, BaseState<EState>>();
    protected BaseState<EState> CurrentState;

    protected bool IsTransitionState = false;

    void Start()
    {
        CurrentState.EnterState();
    }

    void Update()
    {
        EState nextStateKey = CurrentState.GetNextState();

        if (!IsTransitionState && nextStateKey.Equals(CurrentState.StateKey))
        {
            CurrentState.UpdateState();
        }
        else if (!IsTransitionState) {
            TransitionToState(nextStateKey);
        }
        
    }

    public void TransitionToState(EState statekey)
    {
        IsTransitionState = true;
        CurrentState.ExitState();
        CurrentState = States[statekey];
        CurrentState.EnterState();
        IsTransitionState = false;
    }

    void OnTriggerEnter(Collider other)
    {
        CurrentState.OnTriggerEnter(other);
    }

    void OnTriggerStay(Collider other)
    {
        CurrentState.OnTriggerStay(other);
    }

    void OnTriggerExit(Collider other)
    {
        CurrentState.OnTriggerExit(other);
    }
}
