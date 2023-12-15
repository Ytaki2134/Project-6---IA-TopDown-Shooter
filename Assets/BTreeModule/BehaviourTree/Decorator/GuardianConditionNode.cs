using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardConditionNode : DecoratorNode
{
    
    public Func<bool> condition;

    public GuardConditionNode(Node child, Func<bool> condition)
    {
        m_child = child;
        this.condition = condition;
    }

    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if (!condition())
        {
            // Si la condition n'est pas remplie, échec sans exécuter l'enfant.
            return State.Failure;
        }

        // Si la condition est remplie, exécuter l'enfant.
        return child.Update();
    }
}