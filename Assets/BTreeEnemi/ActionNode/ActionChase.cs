using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class ActionChase : ActionNode
{

    private GameObject _enemy;
    private GameObject _tank;

    private Movement _movement;
    private float minDistance;  // Distance minimale à maintenir
    private float maxDistance;
    protected override void OnStart()
    {
        _enemy = blackboard.Get("targetEnemi") as GameObject;
        _tank = blackboard.Get("targetGameObject") as GameObject;
        _movement = blackboard.Get("movement") as Movement;
        maxDistance =blackboard.Get<float>("distanceMax");
        minDistance = blackboard.Get<float>("distanceMin");
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
        float currentDistance = Vector2.Distance(_tank.transform.position, _enemy.transform.position);


        if (currentDistance > maxDistance)
        {

            _movement.RotateAndMoveTowards(_tank.transform, _enemy.transform, 1.5f ,5f ,blackboard);

        }
        else if(currentDistance < minDistance)
        {
            _movement.RotateAndMoveAwayFrom(_tank.transform, _enemy.transform);
        }

        return State.Running;
    }

}
