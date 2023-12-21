using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ActionAvoidObstacle : ActionNode
{

    private GameObject _enemy;
    private GameObject _tank;
    private Movement _movement;
    private Transform _tankTransform;
    private Rigidbody2D _tankRigidbody;
    private float speed;
    private float rotationSpeed;
    private Vector2 avoidanceDirection;


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
        avoidanceDirection = Vector3.Cross(_tankTransform.up, Vector3.forward).normalized; // Choix de la direction d'évitement
        isObstacleDetected = blackboard.Get<bool>("isObstacleDetected");


    }
    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        if (isObstacleDetected)
        {
            RotateTowards(avoidanceDirection);
            MoveInDirection(avoidanceDirection);

            return State.Running;
        }
        else
        {
            return State.Failure;
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
