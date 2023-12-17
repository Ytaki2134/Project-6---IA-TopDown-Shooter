using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionRotationCanon : ActionNode
{
    private int _dist = 100;
    private GameObject _targetToMove;
    private GameObject _me;

    protected override void OnStart()
    {
        _targetToMove = (blackboard.Get("targetEnemi") as GameObject);
        _me = base.blackboard.Get("targetGameObject") as GameObject;
    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {


        if (Vector2.Distance(_targetToMove.GetComponent<Transform>().position, _me.transform.position) < _dist)
        {

            Transform transform = _me.GetComponent<Transform>().GetChild(0).GetComponent<Transform>();

            Vector2 direction = (_targetToMove.GetComponent<Transform>().position - _me.GetComponent<Transform>().GetChild(0).GetComponent<Transform>().position).normalized;
            if (direction != Vector2.zero)
            {
                Quaternion toRot = Quaternion.Euler(0, 0, Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg);
                transform.rotation = Quaternion.Lerp(transform.rotation, toRot, Time.deltaTime * 100);
            }

            return State.Running;
        }
        else
        {
            return State.Failure;
        }
    }
}
