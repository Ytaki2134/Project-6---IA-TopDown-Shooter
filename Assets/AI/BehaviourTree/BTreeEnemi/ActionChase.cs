using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class ActionChase : ActionNode
{

    private GameObject _enemy;
    private GameObject _tank;

    private Movement _movement;
    private float minDistance;  // Distance minimale ? maintenir
    private float maxDistance;
    private AudioManager m_audioManager;
    private Animator _animatorTrack1;
    private Animator _animatorTrack2;
    private float bufferDistance;

    protected override void OnStart()
    {
        _enemy = blackboard.Get("targetEnemi") as GameObject;
        _tank = blackboard.Get("targetGameObject") as GameObject;
        _movement = blackboard.Get("movement") as Movement;
        maxDistance = blackboard.Get<float>("distanceMax");
        minDistance = blackboard.Get<float>("distanceMin");
        m_audioManager = blackboard.Get<AudioManager>("audioManager");
        _animatorTrack1 = blackboard.Get<Animator>("animatorTrack1");
        _animatorTrack2 = blackboard.Get<Animator>("animatorTrack2");
        bufferDistance = blackboard.Get<float>("bufferDistance");

    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if (_enemy == null || _tank == null)
        {
            return State.Failure;
        }
        float currentDistance = Vector2.Distance(_tank.transform.position, _enemy.transform.position);
        if (currentDistance > minDistance + bufferDistance && currentDistance < maxDistance - bufferDistance)
        {
            _movement.SetSpeed(0);
            // Dans la zone tampon, maintenez l'état actuel
            return State.Running;
        }
        else
        {
            _movement.SetSpeed(blackboard.Get<float>("speed"));

        }

        if (currentDistance > maxDistance)
        {

            _movement.RotateAndMoveTowards(_tank.transform, _enemy.transform);
            m_audioManager.PlaySound();
            _animatorTrack1.SetBool("Enable", true);
            _animatorTrack2.SetBool("Enable", true);

        }
        else if (currentDistance < minDistance)
        {
            _movement.RotateAndMoveAwayFrom(_tank.transform, _enemy.transform);
            m_audioManager.PlaySound();

        }
        else
        {
            m_audioManager.StopSound();
            _animatorTrack1.SetBool("Enable", false);
            _animatorTrack2.SetBool("Enable", false);


        }

        return State.Running;
    }

}