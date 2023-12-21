using System.Media;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Summon : ActionNode
{
    private GameObject _me;
    private GameObject[] _summonList;
    protected override void OnStart()
    {
        _me = blackboard.Get("_targetGameObject") as GameObject;
        _summonList = blackboard.Get("Summon") as GameObject[];

    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        if (_me == null )
        {
            return State.Failure;
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {

                _me.GetComponent <SummonTank>().SummonRandom();
            }

            return State.Success;
        }
        return State.Running;
    }

}
