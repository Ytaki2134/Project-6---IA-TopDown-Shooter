using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class NPCConnectedPatrol : MonoBehaviour
{
    //dicates wheter the agent wait on each node
    [SerializeField] bool _patrolWaiting;

    //the patrol time we wait at each node
    [SerializeField] float _totalWaitTime = 3f;

    //the list of switching direcion
    //[SerializeField] float _switchProbability = 0.2f;

    //private variables for bas behavior
    NavMeshAgent _navMeshAgent;
    ConnectedWaypoint _currentWaypoint;
    ConnectedWaypoint _previousWaypoint;

    bool _traveling;
    bool _waiting;
    float _waitTimer;
    int _waypointVisited;

    public void Start()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();

        if (_navMeshAgent == null)
        {
            Debug.Log("The nav mesh agent component is ot attached to " + gameObject.name);
        }
        else
        {
            if (_currentWaypoint == null)
            {
                //set it at random
                //grab all waypoint objects in scene
                GameObject[] allWaypoints = GameObject.FindGameObjectsWithTag("Waypoints");

                if (allWaypoints.Length > 0)
                {
                    while (_currentWaypoint == null)
                    {
                        int random = UnityEngine.Random.Range(0, allWaypoints.Length);
                        ConnectedWaypoint startingWaypoint = allWaypoints[random].GetComponent<ConnectedWaypoint>();

                        //i.e we found a waypoint
                        if (startingWaypoint != null)
                        {
                            _currentWaypoint = startingWaypoint;
                        }
                    }
                }
                else
                {
                    Debug.Log("failed to find any waypoints for use in the scene");
                }
            }
            SetDestination();
        }
    }

    public void Update()
    {
        //check if we're close to the destination
        if (_traveling && _navMeshAgent.remainingDistance <= 1.0f)
        {
            _traveling = false;
            _waypointVisited++;

            //if we're going to wait, then wait
            if (_patrolWaiting)
            {
                _waiting = true;
                _waitTimer = 0f;
            }
            else
            {
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

                SetDestination();
            }
        }
    }

    private void SetDestination()
    {
        if (_waypointVisited > 0)
        {
            ConnectedWaypoint nextWaypoint = _currentWaypoint.NextWaypoint(_previousWaypoint);
            _previousWaypoint = _currentWaypoint;
            _currentWaypoint = nextWaypoint;
        }

        Vector3 targetVector = _currentWaypoint.transform.position;
        _navMeshAgent.SetDestination(targetVector);
        _traveling = true;
    }
}