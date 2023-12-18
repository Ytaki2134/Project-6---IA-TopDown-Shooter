using System;
using UnityEngine;

public enum ConditionType
{
    IsInDistance,
    HealthCheck,
    CanUsePatternOne,
    ShieldGreaterThanZero,
}

[Serializable]
public class Condition
{
    public ConditionType conditionType;
    public bool expectedValue; // Expected value for the condition to be considered true
    public float threshold; // Additional parameter for comparison, like health threshold

    public bool Evaluate(Blackboard blackboard)
    {
        switch (conditionType)
        {
            case ConditionType.IsInDistance:
                {
                    float dist = Vector2.Distance((blackboard.Get("targetGameObject") as GameObject).transform.position, (blackboard.Get("targetEnemi") as GameObject).transform.position);
                    return dist < threshold == expectedValue;
                }

            case ConditionType.HealthCheck:
                {
                    float health = (float)blackboard.Get("health");
                    Debug.Log(health + " > " + threshold + "==" + expectedValue);
                    return health > threshold == expectedValue;
                }

            case ConditionType.CanUsePatternOne:
                {
                    bool canUsePatternOne = (bool)blackboard.Get("canUsePatternOne");
                    return canUsePatternOne == expectedValue;
                }
            case ConditionType.ShieldGreaterThanZero:
                {
                    Shield shieldComponent = blackboard.Get("shield") as Shield;
                    Debug.Log(shieldComponent.ShieldValue + " > " + threshold + "==" + expectedValue);
                    return shieldComponent.ShieldValue > threshold;
                    

                }
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
