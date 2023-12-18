using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
public class ActionChase : ActionNode
{

    private int _dist;
    private GameObject _targetToMove;
    private GameObject _me;


    protected override void OnStart()
    {
        _targetToMove = blackboard._targetToMove;
        _me = blackboard._targetGameObject;
        _dist = 5;
    }

    protected override void OnStop()
    {
       
    }

    protected override State OnUpdate()
    {


        if (Vector2.Distance(_targetToMove.GetComponent<Transform>().position, _me.transform.position) < _dist) {

            _me.GetComponent<Transform>().position = Vector2.MoveTowards(_me.GetComponent<Transform>().position, _targetToMove.GetComponent<Transform>().position
                ,blackboard._speed * Time.deltaTime);
            Vector2 direction = (_targetToMove.GetComponent<Transform>().position - _me.GetComponent<Transform>().position).normalized;
            
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _me.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, angle);
            return State.Running;
        }
        else
        {
            return State.Failure;
        }
    }
}
#endif