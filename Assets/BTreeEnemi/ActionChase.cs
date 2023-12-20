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
        if(Vector2.Distance(_enemy.transform.position, _tank.transform.position)> blackboard.Get<float>("aggroStartDistance")/2f){
            _movement.RotateAndMoveTowards(_tank.transform, _enemy.transform, 1.5f,7f, blackboard);
            return State.Running;
        }
        else
        {
            blackboard.Set("hasChosenAvoidanceDirection", false);

        }
        return State.Running;
      


    }
}
