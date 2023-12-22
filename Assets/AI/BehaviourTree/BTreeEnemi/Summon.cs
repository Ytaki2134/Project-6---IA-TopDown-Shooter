using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Summon : ActionNode
{
    private GameObject _me;
    private GameObject[] _summonList;
    private float _lastSummoneTime = 0f; // Dernier moment où une balle a été tirée
    private float _fireInterval = 1f; // Intervalle entre les tirs en secondes
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
                    //Debug.Log(_audioEnemi != null);
                    _lastSummoneTime = Time.time; // Mettre à jour le dernier temps de tir
            
           
            }

            return State.Running;
        }
    }

}
