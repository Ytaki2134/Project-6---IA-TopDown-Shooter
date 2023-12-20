using UnityEngine;

public class ActionFeignedRetreat : ActionNode
{
    private GameObject _tank;
    private float retreatSpeed;
    private float retreatDuration;
    private float timer;
    private bool isRetreating = true;
    private bool hasCheckedSides = false;
    private float rotationDuration = 2f; // Durée de la rotation
    private float rotationTimer = 0f;

    protected override void OnStart()
    {
        _tank = blackboard.Get<GameObject>("targetGameObject");
        retreatSpeed = blackboard.Get<float>("speed") * 2;
        retreatDuration = 3f;
        timer = 0f;
        rotationTimer = 0f;
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        // Gestion de la retraite
        if (timer < retreatDuration && isRetreating)
        {
            Vector2 retreatDirection = -(_tank.transform.up).normalized;
            _tank.transform.position += (Vector3)retreatDirection * retreatSpeed * Time.deltaTime;

            // Détecter les obstacles
            if (!hasCheckedSides && CheckForObstacle())
            {
                isRetreating = false;
                hasCheckedSides = true;
            }
        }
        else if (hasCheckedSides && rotationTimer < rotationDuration)
        {
            // Effectuer la rotation
            RotateTank();
            rotationTimer += Time.deltaTime;
        }
        else
        {
            // Réinitialisation pour la prochaine activation
            ResetNodeState();
            return State.Success;
        }

        timer += Time.deltaTime;
        return State.Running;
    }

    private bool CheckForObstacle()
    {
        Vector2 backwardDirection = -_tank.transform.up.normalized;
        float raycastDistance = 5f;
        RaycastHit2D hit = Physics2D.Raycast(_tank.transform.position, backwardDirection, raycastDistance);

        return hit.collider != null;
    }

    private void RotateTank()
    {
        // Rotation fixe vers la droite de 90 degrés
        float rotationAngle = 90f;
        Quaternion targetRotation = Quaternion.Euler(0, 0, _tank.transform.eulerAngles.z + rotationAngle);
        _tank.transform.rotation = Quaternion.Lerp(_tank.transform.rotation, targetRotation, Time.deltaTime);
    }

    private void ResetNodeState()
    {
        isRetreating = true;
        hasCheckedSides = false;
        timer = 0f;
        rotationTimer = 0f;
        // Réinitialiser d'autres états si nécessaire
    }
}
