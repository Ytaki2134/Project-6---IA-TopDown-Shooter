using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ActionPatrolEnemi : ActionNode
{

    public GameObject _targetGameObject;
    private GameObject[] _waypoints;
    private float _speed;

    private int _currentWaypointIndex = 0;

    private Movement _movement;


    protected override void OnStart()
    {
        _waypoints = base.blackboard.Get("waypoints") as GameObject[];
        _speed = (float)blackboard.Get("speed");
        _targetGameObject = base.blackboard.Get("targetGameObject") as GameObject;

        _movement = blackboard.Get("movement") as Movement;


    }

    protected override void OnStop()
    {

    }
    protected override State OnUpdate()
    {
        Transform agentTransform = (blackboard.Get("targetGameObject") as GameObject).GetComponent<Transform>();
        Transform waypointTransform = _waypoints[_currentWaypointIndex].GetComponent<Transform>();

        // Effectuer la rotation et le mouvement simultanément
        _movement.RotateAndMoveTowards(agentTransform, waypointTransform, 1.5f,7f, blackboard);

        // Vérifie si le tank est proche du waypoint
        if (Vector2.Distance(agentTransform.position, waypointTransform.position) < 0.01f)
        {
            IncrementWaypointIndex();
        }

        return Node.State.Running;
    }



    private void IncrementWaypointIndex()
    {
        _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
    }


}
