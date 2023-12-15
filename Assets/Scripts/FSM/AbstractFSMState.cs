using Assets.Scripts.NPC;
using UnityEngine;
using UnityEngine.AI;

public abstract class AbstractFSMState : ScriptableObject
{
    protected NavMeshAgent _navMeshAgent;
    protected NPC _npc;

    public ExecutionState ExecutionState { get; protected set; }
    
    public virtual void OnEnable()
    {
        ExecutionState = ExecutionState.NONE;
    }

    public virtual bool EnterState()
    {
        bool successNavMesh = true;
        bool successNPC = true;
        ExecutionState = ExecutionState.ACTIVE;

        //navMeshAgent existe ?
        successNavMesh = (_navMeshAgent != null);

        //executingAgent existe ?
        successNPC = (_npc != null);

        return successNavMesh & successNPC;
    }

    public abstract void UpdateState();

    public virtual bool ExitState()
    {
        ExecutionState = ExecutionState.COMPLETED;
        return true;
    }

    public virtual void SetNavMeshAgent(NavMeshAgent navMeshAgent)
    {
        if(navMeshAgent != null)
        {
            _navMeshAgent = navMeshAgent;
        }
    }

    public virtual void SetExecutingNPC(NPC npc)
    {
        if (_npc != null)
        {
            _npc = npc;
        }
    }
}
