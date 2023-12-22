using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionRotationCanon : ActionNode
{
    private float _oscillationAngle = 45f;
    private GameObject _me;
    private float _angleOffset;
    private float _rotationSpeed; // Vitesse de rotation en degrés par seconde
    private Transform canonTransform;

    private Movement _movement;

    protected override void OnStart()
    {

        _me = blackboard.Get("targetGameObject") as GameObject;
        canonTransform = _me.GetComponent<Transform>().GetChild(0).GetComponent<Transform>();
        _movement = blackboard.Get("movement") as Movement;
        _angleOffset = (float)blackboard.Get("angleOffset");
        _rotationSpeed = (float)blackboard.Get("rotSpeed");
    }

    protected override void OnStop()
    {
        // Toute logique de nettoyage nécessaire
    }

    protected override State OnUpdate()
    {

        // Osciller le canon lorsque l'ennemi est trop loin
        _movement.OscillateCanon((float)blackboard.Get("angleOffset"), (float)blackboard.Get("rotSpeed"), _oscillationAngle, _me.transform, canonTransform, blackboard);
        blackboard.Set("readyToShoot", false);
        return State.Running;
    }

}
