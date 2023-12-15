using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
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
                Vector2 direction = (wp.position - _targetGameObject.GetComponent<Transform>().position).normalized;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                _targetGameObject.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, angle);

            }
        }
        state = Node.State.Running; 
        return state;
    }
}
#endif