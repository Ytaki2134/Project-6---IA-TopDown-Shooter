using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionChase : ActionNode
{

    private GameObject _enemy;
    private GameObject _tank;

    private Movement _movement;


    protected override void OnStart()
    {
        _enemy = blackboard.Get("targetEnemi") as GameObject;
        _tank = blackboard.Get("targetGameObject") as GameObject;
        _movement = blackboard.Get("movement") as Movement;

    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if (_enemy == null || _tank == null)
        {
            return State.Failure;
        }



        //Debug.Log(Vector2.Distance(_enemy.transform.position, _tank.transform.position));
        if (blackboard.Get<bool>("tooNearFromBoss")){
            _movement.RotateAndMoveTowards(_tank.transform, _enemy.transform);
            return State.Running;
        }
        else if(!blackboard.Get<bool>("tooNearFromBoss"))
        {
            _movement.RotateAndMoveAwayFrom(_tank.transform, _enemy.transform);
            return State.Running;

        }
        return State.Running;
      


    }
}
