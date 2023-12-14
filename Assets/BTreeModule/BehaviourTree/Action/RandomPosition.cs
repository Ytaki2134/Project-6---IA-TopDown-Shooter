using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RandomPosition : ActionNode
{
    public Vector2 min = Vector2.one * -10;
    public Vector2 max = Vector2.one * 10;

    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        moveToPosition.x = Random.Range(min.x, max.x);
        moveToPosition.y = Random.Range(min.y, max.y);
        return State.Success;
    }
}
