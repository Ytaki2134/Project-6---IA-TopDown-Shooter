using UnityEngine;

public class ActionRetreatFromObstacle : ActionNode
{
    private GameObject _tank;
    private float retreatSpeed;
    private float checkObstacleCooldown;
    private float lastCheckTime = 0f;
    private float rotationSpeed;
    private bool obstacleDetected = false;
    private float retreatDistance;
    private Vector2 startPosition;

    protected override void OnStart()
    {
        _tank = blackboard.Get<GameObject>("targetGameObject");
        retreatSpeed = blackboard.Get<float>("speed");
        rotationSpeed = blackboard.Get<float>("rotSpeed");
        checkObstacleCooldown = 3f; // Cooldown between obstacle checks
        retreatDistance = blackboard.Get<float>("retreatDistance"); // The distance to retreat before stopping
        startPosition = _tank.transform.position; // Starting position to measure retreat distance
    }

    protected override void OnStop()
    {
        // Reset obstacle detection on stop
        obstacleDetected = false;
    }

    protected override State OnUpdate()
    {
        // Measure the distance retreated
        float distanceRetreated = Vector2.Distance(startPosition, _tank.transform.position);

        // Check if the tank has retreated the required distance and there's no obstacle
        if (distanceRetreated >= retreatDistance && !obstacleDetected)
        {
            // Stop retreating and return success
            return State.Success;
        }

        // If an obstacle has been detected, rotate to avoid it
        if (obstacleDetected)
        {
            RotateTankToRight();
            if (Time.time - lastCheckTime > checkObstacleCooldown)
            {
                // After cooldown, check for obstacles again
                obstacleDetected = CheckForObstacle();
                lastCheckTime = Time.time;
            }
        }
        else
        {
            // No obstacle detected, continue retreating
            Retreat();
            if (Time.time - lastCheckTime > checkObstacleCooldown)
            {
                // After cooldown, check for obstacles
                obstacleDetected = CheckForObstacle();
                lastCheckTime = Time.time;
            }
        }

        return State.Running;
    }

    private void Retreat()
    {
        // Move the tank backwards
        Vector2 retreatDirection = -_tank.transform.up.normalized;
        _tank.transform.position += (Vector3)retreatDirection * retreatSpeed * Time.deltaTime;
    }

    private bool CheckForObstacle()
    {
        // Cast a ray backwards to check for obstacles
        Vector2 backwardDirection = -_tank.transform.up.normalized;
        float raycastDistance = 5f; // or any other appropriate distance
        RaycastHit2D hit = Physics2D.Raycast(_tank.transform.position, backwardDirection, raycastDistance);

        // Return true if an obstacle is detected
        return hit.collider != null;
    }

    private void RotateTankToRight()
    {
        // Rotate the tank to the right by a fixed amount
        _tank.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
