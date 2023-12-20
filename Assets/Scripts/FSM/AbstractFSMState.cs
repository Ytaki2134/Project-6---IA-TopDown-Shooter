using Assets.Scripts.FSM;
using UnityEngine;
using Assets.Scripts.NPCCode;

public abstract class AbstractFSMState : ScriptableObject
{
    protected NPC _npc;
    protected FiniteStateMachine _fsm;

    public ExecutionState ExecutionState { get; protected set; }
    public FSMStateType StateType { get; protected set; }
    public bool EnteredState { get; protected set; }
    
    public virtual void OnEnable()
    {
        ExecutionState = ExecutionState.NONE;
    }

    public virtual bool EnterState()
    {
        bool successNPC = true;
        ExecutionState = ExecutionState.ACTIVE;

        //executingAgent existe ?
        successNPC = (_npc != null);

        return successNPC;
    }

    public abstract void UpdateState();

    public virtual bool ExitState()
    {
        ExecutionState = ExecutionState.COMPLETED;
        return true;
    }

    public virtual void SetExecutingFSM(FiniteStateMachine fsm)
    {
        if (fsm != null)
        {
            _fsm = fsm;
        }
    }

    public virtual void SetExecutingNPC(NPC npc)
    {
        if (npc != null)
        {
            _npc = npc;
        }
    }
}
