using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionChase : ActionNode
{

    private int _dist;
    private float _speed;
    private GameObject _targetToMove;
    private GameObject _me;
    private Movement _movement;


    protected override void OnStart()
    {
        _targetToMove = (blackboard.Get("targetEnemi") as GameObject);
        _me = base.blackboard.Get("targetGameObject") as GameObject;
        _dist = 5;
        _speed = (float)blackboard.Get("speed");
        _movement = blackboard.Get("movement") as Movement;

    }

    protected override void OnStop()
    {
       
    }

    protected override State OnUpdate()
    {


       
            blackboard.Set("speed",0.5f);
            return State.Running;

       
    }
}
