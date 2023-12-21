
using System;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public enum ConditionType
{
    IsInDistance,
    HealthCheck,
    CanUsePatternOne,
    ShieldGreaterThanZero,
    IsInDistanceAndView,
    HasBeenHit
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
                {
                    float dist = Vector2.Distance((blackboard.Get("targetGameObject") as GameObject).transform.position, (blackboard.Get("targetEnemi") as GameObject).transform.position);
                    bool hasAggro = (bool)blackboard.Get("hasAggro");
                    float aggroStartDistance = (float)blackboard.Get("aggroStartDistance");
                    float aggroEndDistance = (float)blackboard.Get("aggroEndDistance");
                    
                    if (!hasAggro && dist <= aggroStartDistance)
                    {
                        blackboard.Set("hasAggro", true); // Prendre l'aggro
                        return true == expectedValue;
                    }
                   //else if ((hasAggro && dist >= aggroEndDistance) || (!hasAggro && dist >= aggroEndDistance))
                   //{
                   //    blackboard.Set("hasAggro", false); // Perdre l'aggro
                   //    return false == expectedValue;
                   //}

                    return hasAggro == expectedValue; // Maintenir l'état actuel de l'aggro
                }

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
            case ConditionType.HasBeenHit:
                bool hasBeenHit = (bool)blackboard.Get("hasBeenHit");
                return hasBeenHit == expectedValue;


            default:
                throw new ArgumentOutOfRangeException();

        }
   
    }
}