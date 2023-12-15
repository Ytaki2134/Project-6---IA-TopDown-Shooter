using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D m_rb;

    private Vector2 m_currentMovement;
    private Quaternion m_targetRotation;

    private float m_speed;
    private float m_brakeSpeedMod;

    private float m_rotationSpeed;
    private float m_brakeRotationSpeedMod;

    private void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    public void Move()
    {
        //Rotate Sprite
        m_targetRotation = Quaternion.Euler(0, 0, Mathf.Atan2(-m_currentMovement.x, m_currentMovement.y) * Mathf.Rad2Deg);
        transform.rotation = Quaternion.Lerp(transform.rotation, m_targetRotation, Time.deltaTime * m_rotationSpeed * m_brakeRotationSpeedMod);

        //Move Player (forward only)
        m_rb.AddForce(transform.up * m_speed * m_brakeSpeedMod);
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