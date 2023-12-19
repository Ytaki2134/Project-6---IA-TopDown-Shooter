using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionChase : ActionNode
{

    private GameObject _enemy;
    private GameObject _tank;
    private float _chaseDistance = 10f;
    private float _speed;

    protected override void OnStart()
    {
        _enemy = blackboard.Get("targetEnemi") as GameObject;
        _tank = blackboard.Get("targetGameObject") as GameObject;
        _speed = (float)blackboard.Get("speed");

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

       //float distanceToEnemy = Vector2.Distance(_tank.transform.position, _enemy.transform.position);
       //if (distanceToEnemy <= _chaseDistance)
       //{
       //    // Assez proche de l'ennemi
       //    return State.Success;
       //}

        // Se rapprocher de l'ennemi
        Vector2 direction = (_enemy.transform.position - _tank.transform.position).normalized;
        _tank.transform.position += (Vector3)direction * _speed * Time.deltaTime;
        return State.Running;


    }
}
