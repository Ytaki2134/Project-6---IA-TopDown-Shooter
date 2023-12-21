using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionChaseV2 : ActionNode
{



    private GameObject _enemy;
    private GameObject _tank;
    private Movement _movement;
    private Transform _tankTransform;
    private Rigidbody2D _tankRigidbody;
    private float speed;
    private float rotationSpeed;
    private bool isObstacleDetected;


    protected override void OnStart()
    {

        _enemy = blackboard.Get("targetEnemi") as GameObject;
        _tank = blackboard.Get("targetGameObject") as GameObject;
        _movement = blackboard.Get("movement") as Movement;

        _tankTransform = _tank.transform;
        _tankRigidbody = _tank.GetComponent<Rigidbody2D>();

        speed = blackboard.Get<float>("speed");
        
        rotationSpeed = blackboard.Get<float>("rotSpeed");
        isObstacleDetected = blackboard.Get<bool>("isObstacleDetected");
    }
    protected override void OnStop()
    {

    }


    protected override State OnUpdate()
    {
        Debug.Log(isObstacleDetected);

        if (!isObstacleDetected)
        {
            return State.Failure;
        }
        else
        {
            Vector2 directionToPlayer = (_enemy.transform.position - _tankTransform.position).normalized;
            RotateTowards(directionToPlayer);
            MoveInDirection(directionToPlayer);

            return State.Running;
        }


       
    }
    private void RotateTowards(Vector2 direction)
    {
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        // L'angle actuel est directement _tankRigidbody.rotation pour un Rigidbody2D
        float smoothAngle = Mathf.LerpAngle(_tankRigidbody.rotation, targetAngle, rotationSpeed * Time.deltaTime);
        _tankRigidbody.MoveRotation(smoothAngle);
    }


    private void MoveInDirection(Vector2 direction)
    {
        _tankRigidbody.MovePosition(_tankRigidbody.position + direction * speed * Time.deltaTime);
    }
}
