
using JetBrains.Annotations;
using System;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public enum ConditionType
{
    IsInDistance,
    HealthCheck,
    CanUsePatternOne,
    ShieldGreaterThanZero,
    IsInDistanceAndView,
    HasBeenHit,
    IsNearObstacle,
    Retreat,

}


[Serializable]
public class Condition
{
    public ConditionType conditionType;
    public bool expectedValue; // La valeur attendue pour que la condition soit considérée comme vraie
    public float threshold;

    public bool Evaluate(Blackboard blackboard)
    {
        GameObject _tank = (blackboard.Get("targetGameObject") as GameObject);
        GameObject _enemi = (blackboard.Get("targetEnemi") as GameObject);
        // Vérifiez la condition en utilisant le type et la valeur du blackboard
        switch (conditionType)
        {
            case ConditionType.IsInDistance:
                {
                    float dist = Vector2.Distance(_tank.transform.position, _enemi.transform.position);
                    bool hasAggro = (bool)blackboard.Get("hasAggro");
                    float aggroStartDistance = (float)blackboard.Get("aggroStartDistance");
                   // float aggroEndDistance = (float)blackboard.Get("aggroEndDistance");
                    
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
            case ConditionType.Retreat:
                bool retreat = blackboard.Get<bool>("retreat");

                return retreat == this.expectedValue; // Use the instance's properties

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
                if (hasBeenHit)
                {
                    blackboard.Set("retreat", true);

                }
                return hasBeenHit == expectedValue;

            case ConditionType.IsNearObstacle:
                {
                    Vector2 backwardDirection = -_tank.transform.up.normalized;
                    float raycastDistance = 10f;
                    float raycastStartOffset = 1.5f; // Un petit décalage pour éviter de détecter son propre collider

                    // Point de départ du raycast, décalé légèrement vers l'arrière pour éviter le collider du tank
                    Vector2 raycastStartPoint = _tank.transform.position + (Vector3)(backwardDirection * raycastStartOffset);

                    RaycastHit2D hit = Physics2D.Raycast(raycastStartPoint, backwardDirection, raycastDistance);


                    if (hit.collider != null)
                    {
                        blackboard.Set("isNearObstacle", true);
                        Debug.Log("Trouvé");
                    }
                    else
                    {
                        blackboard.Set("isNearObstacle", false);
                    }
                    bool isNearObstacle = (bool)blackboard.Get("isNearObstacle");
                    return isNearObstacle == expectedValue;
                }

            default:
                throw new ArgumentOutOfRangeException();

        }
   
    }

   

}