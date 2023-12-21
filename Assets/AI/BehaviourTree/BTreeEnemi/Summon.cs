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
        _me = blackboard.Get("targetGameObject") as GameObject;
        _summonList = blackboard.Get("summonList") as GameObject[];

    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        if (_me == null)
        {
            return State.Failure;
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                Vector3 pos = _me.transform.position;
                switch (i)
                {
                    case 0:
                        pos.x += 2f;
                        pos.y += 2f;
                        break;
                    case 1:
                        pos.x += 2f;
                        pos.y -= 2f;
                        break;
                    case 2:
                        pos.x -= 2f;
                        pos.y -= 2f;
                        break;
                    case 3:
                        pos.x -= 2f;
                        pos.y += 2f;
                        break;
                    default: 
                        break;

                }
                _me.GetComponent<SummonTank>().SummonRandom(pos);
            }

            return State.Success;
        }
    }

}
