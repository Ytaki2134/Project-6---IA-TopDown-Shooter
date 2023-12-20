using UnityEngine;

public class ActionRotateAndTarget : ActionNode
{
    private float _oscillationAngle = 45f;
    private GameObject _targetToMove;
    private GameObject _me;
    private float _angleOffset = 0f;
    private float _rotationSpeed = 30f; // Vitesse de rotation en degrés par seconde
    private Transform canonTransform;
    private float _trackingDistance = 10f; // Distance à partir de laquelle le canon commence à suivre la cible
    private float _alignmentThreshold = 10f;
    private Movement _movement;

    protected override void OnStart()
    {
        _targetToMove = blackboard.Get("targetEnemi") as GameObject;
        _me = blackboard.Get("targetGameObject") as GameObject;
        canonTransform = _me.GetComponent<Transform>().GetChild(0).GetComponent<Transform>();
        _movement = blackboard.Get("movement") as Movement;
        blackboard.Set("angleOffset", _angleOffset);
        blackboard.Set("rotSpeed", _rotationSpeed);


    }

    protected override void OnStop()
    {
        // Réinitialisation ou nettoyage si nécessaire
    }

    protected override State OnUpdate()
    {
        float distanceToTarget = Vector2.Distance(_me.transform.position, _targetToMove.transform.position);


       
        if (distanceToTarget > _trackingDistance)
        {
            // Osciller le canon lorsque l'ennemi est trop loin
            _movement.OscillateCanon((float)blackboard.Get("angleOffset"), (float)blackboard.Get("rotSpeed"), _oscillationAngle, _me.transform, canonTransform, blackboard);
            blackboard.Set("readyToShoot", false);
            return State.Running;
        }
        else
        {
            // Suivre la cible du regard quand elle est à portée
            _movement.RotateCanonTowardsTarget(canonTransform,_targetToMove.transform.position, blackboard, _alignmentThreshold);
            blackboard.Set("readyToMove", true);
            return State.Running;
        }
    }

}
