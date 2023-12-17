using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionRotationCanon : ActionNode
{
    private float _dist = 15f;
    private GameObject _targetToMove;
    private GameObject _me;
    private float _alignmentThreshold = 5f;

    protected override void OnStart()
    {
        _targetToMove = blackboard.Get("targetEnemi") as GameObject;
        _me = blackboard.Get("targetGameObject") as GameObject;
    }

    protected override void OnStop()
    {
        // Toute logique de nettoyage nécessaire
    }

    protected override State OnUpdate()
    {
        Transform canonTransform = _me.GetComponent<Transform>().GetChild(0).GetComponent<Transform>();

        if (_targetToMove == null || IsTargetTooFar(_targetToMove.transform.position, _me.transform.position))
        {
            ResetCanonPosition(canonTransform);
            blackboard.Set("readyToShoot", false);
            return State.Failure;
        }

        RotateCanonTowardsTarget(canonTransform, _targetToMove.transform.position);
        return State.Running;
    }

    private bool IsTargetTooFar(Vector3 targetPosition, Vector3 canonPosition)
    {
        return Vector2.Distance(targetPosition, canonPosition) >= _dist;
    }

    private void ResetCanonPosition(Transform canonTransform)
    {
        canonTransform.rotation = Quaternion.Lerp(canonTransform.rotation, _me.GetComponent<Transform>().rotation, Time.deltaTime * 2);
    }

    private void RotateCanonTowardsTarget(Transform canonTransform, Vector3 targetPosition)
    {
        Vector2 direction = (targetPosition - canonTransform.position).normalized;
        if (direction != Vector2.zero)
        {
            Quaternion toRot = Quaternion.Euler(0, 0, Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg);
            canonTransform.rotation = Quaternion.Lerp(canonTransform.rotation, toRot, Time.deltaTime * 2);

            float angleDiff = Quaternion.Angle(canonTransform.rotation, toRot);
            blackboard.Set("readyToShoot", angleDiff <= _alignmentThreshold);
        }
    }
}
