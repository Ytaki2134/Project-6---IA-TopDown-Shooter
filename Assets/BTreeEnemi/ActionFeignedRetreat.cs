using UnityEngine;

public class ActionFeignedRetreat : ActionNode
{
    private GameObject _enemy;
    private GameObject _tank;

    private float retreatSpeed;
    private float retreatDuration;
    private float timer;
    private Vector2 startRetreatPosition;
    private float retreatMaxDistance;
    protected override void OnStart()
    {
        _enemy = blackboard.Get<GameObject>("targetEnemi");
        _tank = blackboard.Get<GameObject>("targetGameObject");
        retreatSpeed = blackboard.Get<float>("speed") *2;
        retreatDuration = 20f;

        timer = 0f;
    }
    protected override void OnStop()
    {

    }
    protected override State OnUpdate()
    {
        if (IsRetreatTimeOver())
        {
            return State.Success; // Retraite terminée, se préparer à attaquer
        }

        Vector2 direction = CalculateRetreatDirection();
        UpdateTankPosition(direction);

        if (IsMaxRetreatDistanceReached())
        {
            return State.Success; // Distance maximale atteinte, arrêter la retraite
        }

        timer += Time.deltaTime;
        return State.Running;
    }

    private bool IsRetreatTimeOver()
    {
        return timer >= retreatDuration;
    }

    private Vector2 CalculateRetreatDirection()
    {
        Vector2 currentPos = _tank.transform.position;
        Vector2 enemyPos = _enemy.transform.position;
        Vector2 direction = (currentPos - enemyPos).normalized;

        if (Physics2D.Raycast(currentPos, direction, 1.0f).collider != null)
        {
            float turnAngle = Random.value > 0.5f ? 90 : -90;
            direction = Quaternion.Euler(0, 0, turnAngle) * direction;
        }

        return direction;
    }

    private void UpdateTankPosition(Vector2 direction)
    {
        Vector2 currentPos = _tank.transform.position;
        _tank.transform.position = currentPos + direction * retreatSpeed * Time.deltaTime;
    }

    private bool IsMaxRetreatDistanceReached()
    {
        return Vector2.Distance(startRetreatPosition, _tank.transform.position) > retreatMaxDistance;
    }

}
