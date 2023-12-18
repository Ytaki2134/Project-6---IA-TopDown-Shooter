
using System;
using UnityEngine;

public enum ConditionType
{
    IsInDistance,

}

[Serializable]
public class Condition
{
    public ConditionType conditionType;
    public bool expectedValue; // La valeur attendue pour que la condition soit considérée comme vraie

    public bool Evaluate(Blackboard blackboard)
    {
        // Vérifiez la condition en utilisant le type et la valeur du blackboard
        switch (conditionType)
        {
            case ConditionType.IsInDistance:
                float dist = Vector2.Distance((blackboard.Get("targetGameObject") as GameObject).transform.position, (blackboard.Get("targetEnemi") as GameObject).transform.position);
                expectedValue = dist < 10;
                return (bool)blackboard.Get("IsInDistance") == expectedValue;

        }
        return false;
    }
}