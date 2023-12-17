using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionChase : ActionNode
{

    private int _dist;
    private float _speed;
    private GameObject _targetToMove;
    private GameObject _me;


    protected override void OnStart()
    {
        _targetToMove = (blackboard.Get("targetEnemi") as GameObject);
        _me = base.blackboard.Get("targetGameObject") as GameObject;
        _dist = 5;
        _speed = (float)blackboard.Get("speed");
    }

    protected override void OnStop()
    {
       
    }

    protected override State OnUpdate()
    {


        if (Vector2.Distance(_targetToMove.GetComponent<Transform>().position, _me.transform.position) < _dist) {

            _me.GetComponent<Transform>().position = Vector2.MoveTowards(_me.GetComponent<Transform>().position, _targetToMove.GetComponent<Transform>().position
                ,_speed * Time.deltaTime);
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
