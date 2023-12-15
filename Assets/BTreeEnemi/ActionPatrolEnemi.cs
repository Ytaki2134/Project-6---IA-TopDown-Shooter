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


    protected override void OnStart()
    {
        _waypoints = base.blackboard._waypoints;
        _speed = base.blackboard._speed;
        _targetGameObject = base.blackboard._targetGameObject;
    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        if (_waiting)
        {
            _waitCounter += Time.deltaTime;
            if(_waitCounter >= _waitTime)
            {
                _waiting = false;
            }
        }
        else
        {
            Transform wp = _waypoints[_currentWaypointIndex].GetComponent<Transform>();

            if(Vector2.Distance(_targetGameObject.GetComponent<Transform>().position, wp.position) < 0.01f)
            {
                _targetGameObject.GetComponent<Transform>().position = wp.position;
                _waitCounter = 0f;
                _waiting = true;

                _currentWaypointIndex++; // Passer au waypoint suivant
                if (_currentWaypointIndex >= _waypoints.Length) // Si fin du tableau atteinte
                {
                    _currentWaypointIndex = 0; // Repartir du premier waypoint ou gérer autrement
                }
            }
            else
            {
                _targetGameObject.GetComponent<Transform>().position  = Vector2.MoveTowards(_targetGameObject.GetComponent<Transform>().position, wp.position, _speed * Time.deltaTime);


                Transform transform = _targetGameObject.GetComponent<Transform>();

                Vector2 direction = (wp.GetComponent<Transform>().position - _targetGameObject.GetComponent<Transform>().position).normalized;
                if (direction != Vector2.zero)
                {
                    Quaternion toRot = Quaternion.Euler(0, 0, Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg);
                    transform.rotation = Quaternion.Lerp(transform.rotation, toRot, Time.deltaTime * 100);
                }

            }
        }
        state = Node.State.Running; 
        return state;
    }
}
