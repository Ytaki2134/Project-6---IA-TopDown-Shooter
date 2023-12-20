using UnityEngine;
public class ActionRotateTank : ActionNode
{
    private GameObject _tank;
    private float rotationSpeed = 60f;

    protected override void OnStart()
    {
        _tank = blackboard.Get<GameObject>("targetGameObject");
    }
    protected override void OnStop()
    {

    }
    protected override State OnUpdate()
    {
        bool shouldRotate = (bool)blackboard.Get("rotate");
 
        return State.Running;
    }
}
