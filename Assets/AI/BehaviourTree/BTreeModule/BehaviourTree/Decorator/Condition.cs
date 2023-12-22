
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
    IsObstacleDetected,

}


[Serializable]
public class Condition
{
    public ConditionType conditionType;
    public bool expectedValue; // Expected value for the condition to be considered true
    public float threshold; // Additional parameter for comparison, like health threshold

    public bool Evaluate(Blackboard blackboard)
    {
        GameObject _enemi = blackboard.Get("targetEnemi") as GameObject;
        GameObject _tank = blackboard.Get("targetGameObject") as GameObject;
        switch (conditionType)
        {
            case ConditionType.IsInDistance:
                {
                    if(_enemi == null)
                    {
                        blackboard.Set("hasAggro", false);
                        return false == expectedValue;
                    }
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

                    return hasAggro == expectedValue; // Maintenir l'�tat actuel de l'aggro
                }
            case ConditionType.IsObstacleDetected:
                {
                    Vector2 direction = _tank.transform.up; // Assumant que l'avant du tank est son 'up'
                    Vector2 raycastStartPoint = _tank.transform.position + (Vector3)(direction * 1.5f);
                    RaycastHit2D hit = Physics2D.Raycast(raycastStartPoint, direction, 5f);

                    // Debugging visuel
                    Debug.DrawLine(raycastStartPoint, raycastStartPoint + direction * 5f, hit.collider != null ? Color.red : Color.green, 0.1f);

                    // Debugging pour voir quel objet est touch�
                    if (hit.collider != null)
                    {
                        Debug.Log("Hit: " + hit.collider.gameObject.name);
                    }

                    Debug.Log("isOb " + (hit.collider != null) + " obs " + expectedValue);

                    blackboard.Set("isObstacleDetected", hit.collider != null);
                    return hit.collider != null == expectedValue;
                }

            case ConditionType.Retreat:
                bool retreat = blackboard.Get<bool>("retreat");

                return retreat == expectedValue; // Use the instance's properties

            case ConditionType.HealthCheck:
                float health = (float)blackboard.Get("health");
                return health > threshold == expectedValue; // Use the instance's properties

            case ConditionType.CanUsePatternOne:
                bool canUsePatternOne = (bool)blackboard.Get("canUsePatternOne");
                return canUsePatternOne == expectedValue; // Use the instance's properties

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
                    float raycastStartOffset = 1.5f; // Un petit d�calage pour �viter de d�tecter son propre collider

                    // Point de d�part du raycast, d�cal� l�g�rement vers l'arri�re pour �viter le collider du tank
                    Vector2 raycastStartPoint = _tank.transform.position + (Vector3)(backwardDirection * raycastStartOffset);

                    RaycastHit2D hit = Physics2D.Raycast(raycastStartPoint, backwardDirection, raycastDistance);


                    if (hit.collider != null)
                    {
                        blackboard.Set("isNearObstacle", true);
                        Debug.Log("Trouv�");
                    }
                    else
                    {
                        blackboard.Set("isNearObstacle", false);
                    }
                    bool isNearObstacle = (bool)blackboard.Get("isNearObstacle");
                    return isNearObstacle == expectedValue;

                }
            default:
                {

                    float dist = Vector2.Distance((blackboard.Get("targetGameObject") as GameObject).transform.position, (blackboard.Get("targetEnemi") as GameObject).transform.position);
                    return dist < threshold == expectedValue;
                }

        }

    }



}