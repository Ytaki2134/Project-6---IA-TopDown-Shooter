using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ActionPatrolEnemi : ActionNode
{

    public GameObject _targetGameObject;
    private GameObject[] _waypoints;
    private float _speed;

    private int _currentWaypointIndex = 0;
    private float _waitTime = 1f;
    private float _waitCounter = 0f;
    private bool _waiting = false;
    private bool _isRotating =true;
    private Quaternion _toRot;
    Transform _transform;

 
    protected override void OnStart()
    {
        _waypoints = base.blackboard.Get("waypoints") as GameObject[];
        _speed = (float)blackboard.Get("speed");
        _targetGameObject = base.blackboard.Get("targetGameObject") as GameObject;
        _transform = _targetGameObject.GetComponent<Transform>();
        PrepareRotation(_transform, _waypoints[_currentWaypointIndex].GetComponent<Transform>());

    }

    protected override void OnStop()
    {

    }
    protected override State OnUpdate()
    {
        Transform agentTransform = (blackboard.Get("targetGameObject") as GameObject).GetComponent<Transform>();
        Transform enemyTransform = (blackboard.Get("targetEnemi") as GameObject).GetComponent<Transform>();

        // Convertir la position 3D en 2D en ignorant la composante z
        Vector2 enemyPosition2D = new Vector2(enemyTransform.position.x, enemyTransform.position.y);
        Vector2 agentPosition2D = new Vector2(agentTransform.position.x, agentTransform.position.y);

        // Distance actuelle entre l'Agent et l'Ennemi
        float currentDistance = Vector2.Distance(enemyPosition2D, agentPosition2D);

     

        if (_isRotating)
        {
            // Effectue la rotation vers le waypoint ciblé
            RotateTowardsWaypoint(_transform);
        }
        else if (_waiting)
        {
            // Gère l'attente sur le waypoint actuel
            HandleWaiting(_transform);
        }
        else
        {
            // Gère le mouvement vers le waypoint actuel
            HandleMovement(_transform);
        }

        
        return Node.State.Running;
     
        
    }

    private void PrepareRotation(Transform transform, Transform waypoint)
    {
        // Calcule la direction vers le waypoint et prépare la rotation
        Vector2 direction = (waypoint.position - transform.position).normalized;
        if (direction != Vector2.zero)
        {
            // Calcule la rotation nécessaire pour faire face au waypoint
            _toRot = Quaternion.Euler(0, 0, Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg);
            _isRotating = true; // Active l'état de rotation
        }
    }

    private void RotateTowardsWaypoint(Transform transform)
    {
        // Vérifie si la rotation est complétée
        if (Quaternion.Angle(transform.rotation, _toRot) < 0.01f)
        {
            _isRotating = false; // Désactive l'état de rotation
        }
        else
        {
            // Effectue la rotation progressivement vers le waypoint
            transform.rotation = Quaternion.Lerp(transform.rotation, _toRot, Time.deltaTime * _speed);
        }
    }

    private void HandleWaiting(Transform transform)
    {
        // Incrémente le compteur d'attente
        _waitCounter += Time.deltaTime;
        if (_waitCounter >= _waitTime)
        {
            // Réinitialise le compteur et termine l'attente
            _waitCounter = 0f;
            _waiting = false;
            IncrementWaypointIndex(); // Passe au waypoint suivant
            PrepareRotation(transform, _waypoints[_currentWaypointIndex].GetComponent<Transform>());
        }
    }

    private void IncrementWaypointIndex()
    {
        // Incrémente ou réinitialise l'index du waypoint
        _currentWaypointIndex++;
        if (_currentWaypointIndex >= _waypoints.Length)
        {
            _currentWaypointIndex = 0; // Revient au premier waypoint si nécessaire
        }
    }

    private void HandleMovement(Transform transform)
    {
        // Obtenir la position actuelle du waypoint
        Transform wp = _waypoints[_currentWaypointIndex].GetComponent<Transform>();
        // Vérifie si le tank est proche du waypoint
        if (Vector2.Distance(transform.position, wp.position) < 0.01f)
        {
            // Commence à attendre si le waypoint est atteint
            if (!_waiting)
            {
                _waiting = true;
                _waitCounter = 0f; // Réinitialise le compteur d'attente
            }
        }
        else
        {
            // Déplace le tank vers le waypoint
            transform.position = Vector2.MoveTowards(transform.position, wp.position, _speed * Time.deltaTime);
        }
    }

}
