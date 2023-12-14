using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ActionPatrolEnemi : ActionNode
{
    [SerializeReference]
    public GameObject _targetGameObject;
    
    [SerializeField]
    private GameObject[] _waypoints;
    private int _currentWaypointIndex = 0;
    private float _waitTime = 1f;
    private float _waitCounter = 0f;
    private bool _waiting = false;
    [SerializeField]
    private float _speed;

    protected override void OnStart()
    {
        
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
            Debug.Log(wp.position);

            if(Vector2.Distance(_targetGameObject.GetComponent<Transform>().position, wp.position) < 0.01f)
            {
            Debug.Log(_targetGameObject.GetComponent<Transform>().position);
                _targetGameObject.GetComponent<Transform>().position = wp.position;
                _waitCounter = 0f;
                _waiting = true;
            }
            else
            {
                Debug.Log(_targetGameObject.GetComponent<Transform>().position);
                _targetGameObject.GetComponent<Transform>().position  = Vector2.MoveTowards(_targetGameObject.GetComponent<Transform>().position, wp.position, _speed * Time.deltaTime);
                Debug.Log(_targetGameObject.GetComponent<Transform>().position);

                _targetGameObject.GetComponent<Transform>().LookAt(wp.position);
            }
        }
        state = Node.State.Running; 
        return state;
    }
}
