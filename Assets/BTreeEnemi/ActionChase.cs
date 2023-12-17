using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionChase : ActionNode
{

    private int _dist;
    private GameObject _targetToMove;

    Transform agentTransform;
    Transform enemyTransform;



    protected override void OnStart()
    {
        _dist = 7;
         agentTransform = (blackboard.Get("targetGameObject") as GameObject).GetComponent<Transform>();
         enemyTransform = (blackboard.Get("targetEnemi") as GameObject).GetComponent<Transform>();
        // Initialiser les coins du carré
        InitializeCorners(enemyTransform.position);

    }

    protected override void OnStop()
    {
       
    }

    private enum SquareState
    {
        MovingToCorner,
        RotatingToNextCorner
    }

    private SquareState _squareState = SquareState.MovingToCorner;
    private int _currentCornerIndex = 0;
    private List<Vector2> _corners = new List<Vector2>();
    private float _rotationSpeed = 90f; // Vitesse de rotation en degrés par seconde
    private float _angleToRotate = 0f;


    protected override State OnUpdate()
    {
        switch (_squareState)
        {
            case SquareState.MovingToCorner:
                MoveToCorner(agentTransform);
                break;
            case SquareState.RotatingToNextCorner:
                RotateToNextCorner(agentTransform);
                break;
        }

        return State.Running;
    }

    private void InitializeCorners(Vector3 center)
    {
        float sideLength = 10f; // La longueur du côté du carré

        _corners.Add(center + new Vector3(sideLength / 2, sideLength / 2, 0f));
        _corners.Add(center + new Vector3(-sideLength / 2, sideLength / 2, 0f));
        _corners.Add(center + new Vector3(-sideLength / 2, -sideLength / 2, 0f));
        _corners.Add(center + new Vector3(sideLength / 2, -sideLength / 2, 0f));
    }

    private void MoveToCorner(Transform transform)
    {
        Vector2 currentCorner = _corners[_currentCornerIndex];
        transform.position = Vector2.MoveTowards(transform.position, currentCorner, 2 * Time.deltaTime);

        if (Vector2.Distance(transform.position, currentCorner) < 0.01f) // Coin atteint
        {
            _squareState = SquareState.RotatingToNextCorner;
            _currentCornerIndex = (_currentCornerIndex + 1) % _corners.Count;
            _angleToRotate = 90f; // Préparer pour une rotation de 90 degrés
        }
    }

    private void RotateToNextCorner(Transform transform)
    {
        if (_angleToRotate > 0f)
        {
            float rotationThisFrame = _rotationSpeed * Time.deltaTime;
            transform.Rotate(new Vector3(0, 0, rotationThisFrame));
            _angleToRotate -= rotationThisFrame;
        }
        else
        {
            _squareState = SquareState.MovingToCorner;
        }
    }




}
