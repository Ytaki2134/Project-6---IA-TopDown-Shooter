
using System;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public enum ConditionType
{
    IsInDistance,
    HealthCheck,
    CanUsePatternOne,
    ShieldGreaterThanZero, // Ajout du nouveau type de condition
}


[Serializable]
public class Condition
{
    public ConditionType conditionType;
    public bool expectedValue; // La valeur attendue pour que la condition soit considérée comme vraie
    public float threshold;
    public bool Evaluate(Blackboard blackboard)
    {
        // Vérifiez la condition en utilisant le type et la valeur du blackboard
        switch (conditionType)
        {
            case ConditionType.IsInDistance:
                float dist = Vector2.Distance((blackboard.Get("targetGameObject") as GameObject).transform.position, (blackboard.Get("targetEnemi") as GameObject).transform.position);
                expectedValue = dist < 10;
                return (bool)blackboard.Get("IsInDistance") == expectedValue;
            case ConditionType.HealthCheck:
                float health = (float)blackboard.Get("health");
                return health > this.threshold == this.expectedValue; // Use the instance's properties

            case ConditionType.CanUsePatternOne:
                bool canUsePatternOne = (bool)blackboard.Get("canUsePatternOne");
                return canUsePatternOne == this.expectedValue; // Use the instance's properties

            case ConditionType.ShieldGreaterThanZero:
                {
                    Shield shieldComponent = blackboard.Get("shield") as Shield;
                    if (shieldComponent != null)
                    {
                        return shieldComponent.ShieldValue > 0 == expectedValue;
                    }
                    else
                    {
                        Debug.LogError("Shield component is not found on the blackboard.");
                        return false;
                    }
                }

            default:
                throw new ArgumentOutOfRangeException();

        }
   
    }
}