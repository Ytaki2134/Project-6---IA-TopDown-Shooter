using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private GameObject m_Player;

    private Vector2 m_currentMovement;
    private Quaternion m_targetRotation;

    private float m_speed;
    private float m_brakeSpeedMod;

    private float m_rotationSpeed;
    private float m_brakeRotationSpeedMod;
    private void Start()
    {

    }
    public void Move()
    {
        //Rotate Sprite
        m_targetRotation = Quaternion.Euler(0, 0, Mathf.Atan2(-m_currentMovement.x, m_currentMovement.y) * Mathf.Rad2Deg);
        transform.rotation = Quaternion.Lerp(transform.rotation, m_targetRotation, Time.deltaTime * m_rotationSpeed * m_brakeRotationSpeedMod);

        //Move Player (forward only)
        m_Player.GetComponent<Rigidbody2D>().AddForce(transform.up * m_speed * m_brakeSpeedMod);
    }

    public bool IsTargetTooFar(Vector3 targetPosition, Vector3 canonPosition, float dist)
    {
        return Vector2.Distance(targetPosition, canonPosition) >= dist;
    }

    public void ResetCanonPosition(Transform canonTransform, GameObject me)
    {
        canonTransform.rotation = Quaternion.Lerp(canonTransform.rotation, me.GetComponent<Transform>().rotation, Time.deltaTime * m_rotationSpeed);

    }

    public void RotateCanonTowardsTarget(Transform canonTransform, Vector3 targetPosition, Blackboard blackboard, float alignmentThreshold)
    {
        Vector2 direction = (targetPosition - canonTransform.position).normalized;
        if (direction != Vector2.zero)
        {
            Quaternion toRot = Quaternion.Euler(0, 0, Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg);
            canonTransform.rotation = Quaternion.Lerp(canonTransform.rotation, toRot, Time.deltaTime * m_rotationSpeed);


            float angleDiff = Quaternion.Angle(canonTransform.rotation, toRot);
            blackboard.Set("readyToShoot", angleDiff <= alignmentThreshold);

        }

    }
    public void OscillateCanon(float angleOffset, float rotSpeed, float oscillationAngle, Transform mySelff, Transform canonTransform, Blackboard blackboard)
    {
        // Mise � jour de l'angle d'oscillation
        angleOffset += rotSpeed * Time.deltaTime;

        if (angleOffset > oscillationAngle || angleOffset < -oscillationAngle)
        {
            rotSpeed = -rotSpeed; // Inverser la direction de l'oscillation
        }

        // Appliquer l'oscillation
        Quaternion targetRotation = Quaternion.Euler(0, 0, mySelff.eulerAngles.z + angleOffset);
        canonTransform.rotation = Quaternion.Lerp(canonTransform.rotation, targetRotation, Time.deltaTime * m_rotationSpeed);
        blackboard.Set("angleOffset", angleOffset);
        blackboard.Set("rotSpeed", rotSpeed);

    }
    private Vector2 avoidanceDirection;
    private float avoidanceCooldown = 1f; // Temps en secondes avant de pouvoir changer � nouveau la direction d'�vitement

    // public void RotateAndMoveTowards(Transform agentTransform, Transform waypointTransform, float raycastOffset, float raycastDistance, Blackboard blackboard)
    // {
    //     float lastAvoidanceTime = blackboard.Get<float>("lastAvoidanceTime");
    //     Vector2 direction = (waypointTransform.position - agentTransform.position).normalized;
    //     Quaternion targetRotation = Quaternion.Euler(0, 0, Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg);
    //
    //     Vector2 raycastStartPoint = agentTransform.position + (Vector3)(direction * raycastOffset);
    //     RaycastHit2D hit = Physics2D.Raycast(raycastStartPoint, direction, raycastDistance);
    //
    //     bool isObstacleDetected = hit.collider != null && hit.collider.gameObject != agentTransform.gameObject;
    //     blackboard.Set("isObstacleDetected", isObstacleDetected);
    //
    //     if (isObstacleDetected && Time.time - lastAvoidanceTime > avoidanceCooldown)
    //     {
    //         Debug.Log("Obstable");
    //         if (!blackboard.Get<bool>("hasChosenAvoidanceDirection"))
    //         {
    //             Debug.Log("Direcs");
    //
    //             bool isObstacleOnRight = Vector3.Cross(direction, hit.point - (Vector2)agentTransform.position).z < 0;
    //             float avoidanceAngle = isObstacleOnRight ? -90 : 90;
    //             avoidanceDirection = Quaternion.Euler(0, 0, avoidanceAngle) * direction;
    //             blackboard.Set("hasChosenAvoidanceDirection", true);
    //
    //             lastAvoidanceTime = Time.time;
    //             blackboard.Set("lastAvoidanceTime", lastAvoidanceTime);
    //
    //             // Appliquer directement la rotation cible pour un changement rapide de direction
    //             targetRotation = Quaternion.Euler(0, 0, Mathf.Atan2(-avoidanceDirection.x, avoidanceDirection.y) * Mathf.Rad2Deg);
    //             agentTransform.rotation = targetRotation;
    //         }
    //     }
    //     else if (blackboard.Get<bool>("hasChosenAvoidanceDirection") && !blackboard.Get<bool>("isObstacleDetected"))
    //     {
    //         blackboard.Set("hasChosenAvoidanceDirection", false);
    //     }
    //     else
    //     {
    //         // Appliquer une rotation progressive vers la direction cible
    //         agentTransform.rotation = Quaternion.Lerp(agentTransform.rotation, targetRotation, Time.deltaTime * m_rotationSpeed);
    //     }
    //
    //     agentTransform.position = Vector2.MoveTowards(agentTransform.position, (Vector2)agentTransform.position + direction, m_speed * Time.deltaTime);
    // }

    public void RotateAndMoveTowards(Transform agentTransform, Transform waypointTransform)
    {
        Vector2 direction = (waypointTransform.position - agentTransform.position).normalized;
        Quaternion targetRotation = Quaternion.Euler(0, 0, Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg);

        agentTransform.rotation = Quaternion.Lerp(agentTransform.rotation, targetRotation, Time.deltaTime * m_rotationSpeed);
        agentTransform.position = Vector2.MoveTowards(agentTransform.position, waypointTransform.position, m_speed * Time.deltaTime);
    }

    public void RotateAndMoveAwayFrom(Transform agentTransform, Transform waypointTransform)
    {
        // Calculer la direction oppos�e par rapport au waypoint
        Vector2 direction = (agentTransform.position - waypointTransform.position).normalized;

        // Calculer la rotation cible pour faire face � l'oppos� du waypoint
        Quaternion targetRotation = Quaternion.Euler(0, 0, Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg);

        // Appliquer la rotation
        agentTransform.rotation = Quaternion.Lerp(agentTransform.rotation, targetRotation, Time.deltaTime * m_rotationSpeed);

        // D�placer l'agent dans la direction oppos�e
        Vector3 newDirection = new Vector3(direction.x, direction.y, 0); // Conversion de Vector2 en Vector3
        agentTransform.position = Vector2.MoveTowards(agentTransform.position, agentTransform.position + newDirection, m_speed * Time.deltaTime);
    }

    #region Setters

    public void SetCurrentMovement(Vector2 currentMovement)
    {
        m_currentMovement = currentMovement;
    }

    public void SetTargetRotation(Quaternion targetRotation)
    {
        m_targetRotation = targetRotation;
    }

    public void SetSpeed(float speed)
    {
        m_speed = speed;
    }

    public void SetBrakeSpeed(float brakeSpeed)
    {
        m_brakeSpeedMod = brakeSpeed;
    }

    public void SetRotationSpeed(float rotationSpeed)
    {
        m_rotationSpeed = rotationSpeed;
    }

    public void SetBrakeRotationSpeed(float brakeRotationSpeed)
    {
        m_brakeRotationSpeedMod = brakeRotationSpeed;
    }

    #endregion
}