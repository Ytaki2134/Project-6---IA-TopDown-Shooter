using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSightFollowTarget : ActionNode
{

    private GameObject _targetToMove;
    private GameObject _me;
    private Transform canonTransform;
    private float _alignmentThreshold = 10f;
    private Movement _movement;

    protected override void OnStart()
    {
        _targetToMove = blackboard.Get("targetEnemi") as GameObject;
        _me = blackboard.Get("targetGameObject") as GameObject;
        canonTransform = _me.GetComponent<Transform>().GetChild(0).GetComponent<Transform>();
        _movement = blackboard.Get("movement") as Movement;


    }

    protected override void OnStop()
    {
        // Toute logique de nettoyage nécessaire
    }

    protected override State OnUpdate()
    {
        // Suivre la cible du regard quand elle est à portée
        _movement.RotateCanonTowardsTarget(canonTransform, _targetToMove.transform.position, blackboard, _alignmentThreshold);
        blackboard.Set("readyToMove", true);
        return State.Running;
    }

}
