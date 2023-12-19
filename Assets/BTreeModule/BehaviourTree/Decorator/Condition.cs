
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
}


[Serializable]
public class Condition
{
    public ConditionType conditionType;
    public bool expectedValue; // La valeur attendue pour que la condition soit consid�r�e comme vraie
    public float threshold;
    public bool Evaluate(Blackboard blackboard)
    {
        // V�rifiez la condition en utilisant le type et la valeur du blackboard
        switch (conditionType)
        {
            case ConditionType.IsInDistance:
                {
                    float dist = Vector2.Distance((blackboard.Get("targetGameObject") as GameObject).transform.position, (blackboard.Get("targetEnemi") as GameObject).transform.position);

                    return dist < threshold == expectedValue;
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
            case ConditionType.IsInDistanceAndView:
                {
                    GameObject targetGameObject = blackboard.Get<GameObject>("targetGameObject");
                    GameObject targetEnemi = blackboard.Get<GameObject>("targetEnemi");
                    float angleOffset = blackboard.Get<float>("angleOffset");

                    Vector2 directionToEnemy = (targetEnemi.transform.position - targetGameObject.transform.position).normalized;
                    Vector2 tankForward = targetGameObject.transform.up; // ou .right selon l'orientation du tank
                    float angleToEnemy = Vector2.Angle(tankForward, directionToEnemy);

                    // V�rifiez si l'ennemi est � port�e et dans l'angle de vue
                    float dist = Vector2.Distance(targetGameObject.transform.position, targetEnemi.transform.position);
                    bool isInDistance = dist < threshold;
                    bool isInView = angleToEnemy <= angleOffset;

                    return (isInDistance && isInView) == expectedValue;
                }
                

            default:
                throw new ArgumentOutOfRangeException();

        }
   
    }
}