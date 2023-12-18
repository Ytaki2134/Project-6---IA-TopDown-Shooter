using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionRotationCanon : ActionNode
{
    private float _dist = 15f;
    private GameObject _targetToMove;
    private GameObject _me;
    private Movement _movement;
    private float _alignmentThreshold = 5f;

    protected override void OnStart()
    {
        _targetToMove = blackboard.Get("targetEnemi") as GameObject;
        _me = blackboard.Get("targetGameObject") as GameObject;
        _movement = blackboard.Get("movement") as Movement;

    }

    protected override void OnStop()
    {
        // Toute logique de nettoyage nécessaire
    }

    protected override State OnUpdate()
    {
        Transform canonTransform = _me.GetComponent<Transform>().GetChild(0).GetComponent<Transform>();

        if (_targetToMove == null || _movement.IsTargetTooFar(_targetToMove.transform.position, _me.transform.position, _dist))
        {
            _movement.ResetCanonPosition(canonTransform, _me);
            blackboard.Set("readyToShoot", false);
            return State.Failure;
        }

        _movement.RotateCanonTowardsTarget(canonTransform, _targetToMove.transform.position, blackboard,_alignmentThreshold);
        return State.Running;
    }

}
