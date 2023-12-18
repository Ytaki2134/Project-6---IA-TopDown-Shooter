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
        // Mise à jour de l'angle d'oscillation
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
