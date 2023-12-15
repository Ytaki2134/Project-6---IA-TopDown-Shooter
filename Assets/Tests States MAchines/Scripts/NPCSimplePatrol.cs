using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCSimplePatrol : MonoBehaviour
{
    //dicates wheter the agent wait on each node
    [SerializeField] bool _patrolWaiting;

    //the patrol time we wait at each node
    [SerializeField] float _totalWaitTime = 3f;

    //the list of switching direcion
    [SerializeField] float _switchProbability = 0.2f;

    //the list of patrol nods to visit
    [SerializeField] List<Waypoint> _patrolPoints;

    //private variables for bas behavior
    NavMeshAgent _navMeshAgent;
    int _currentPatrolIndex;
    bool _traveling;
    bool _waiting;
    bool _patrolForward;
    float _waitTimer;

    public void Start()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();

        if (_navMeshAgent ==  null)
        {
            Debug.Log("The av mesh agent component is ot attached to " + gameObject.name);
        }
        else
        {
            if (_patrolPoints !=  null && _patrolPoints.Count >= 2)
            {
                _currentPatrolIndex = 0;
                SetDestination();
            }
            else
            {
                Debug.Log("Insufficient patrol points for basic patrolling behavior");
            }
        }
    }

    public void Update()
    {
        //check if we're close to the destination
        if (_traveling && _navMeshAgent.remainingDistance <= 1.0f)
        {
            _traveling = false;

            //if we're going to wait, then wait
            if (_patrolWaiting)
            {
                _waiting = true;
                _waitTimer = 0f;
            }
            else
            {
                ChangePatrolPoint();
                SetDestination();
            }
        }

        //instead if we're waiting
        if (_waiting)
        {
            _waitTimer += Time.deltaTime;
            if (_waitTimer >= _totalWaitTime)
            {
                _waiting = false;

                ChangePatrolPoint() ;
                SetDestination();
            }
        }
    }

    private void SetDestination()
    {
        if (_patrolPoints != null)
        {
            Vector3 targetVector = _patrolPoints[_currentPatrolIndex].transform.position;
            _navMeshAgent.SetDestination(targetVector);
            _traveling = true;
        }
    }

    //select a new patrol point in the available list, but also with a small probability allows for us to move forward or backwards
    private void ChangePatrolPoint()
    {
        if (UnityEngine.Random.Range(0f, 1f) <= _switchProbability)
        {
            _patrolForward = !_patrolForward;
        }

        if (_patrolForward)
        {
            _currentPatrolIndex++;

            if (_currentPatrolIndex >= _patrolPoints.Count)
            {
                _currentPatrolIndex = 0;
            }
            //_currentPatrolIndex = (_currentPatrolIndex + 1) % _patrolPoints.Count;
        }
        else
        {
            if (--_currentPatrolIndex < 0)
            {
                _currentPatrolIndex = _patrolPoints.Count - 1;
            }
        }
    }
}