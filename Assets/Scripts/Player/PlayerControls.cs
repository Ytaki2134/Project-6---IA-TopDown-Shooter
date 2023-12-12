using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private PlayerInputs m_inputs;
    private Statistics m_statistics;
    private Movement m_movement;

    void Awake()
    {
        m_inputs = new PlayerInputs();
        m_statistics = GetComponent<Statistics>();
        m_movement = GetComponent<Movement>();

        m_movement.SetSpeed(0f);
        m_movement.SetRotationSpeed(0f);
        m_movement.SetBrakeSpeed(1f);
        m_movement.SetBrakeRotationSpeed(1f);

        #region Input Definitions

        m_inputs.Player.Move.performed += ctx =>
        {
            m_movement.SetSpeed(m_statistics.Speed);
            m_movement.SetRotationSpeed(m_statistics.RotationSpeed);
            m_movement.SetCurrentMovement(ctx.ReadValue<Vector2>());
        };

        m_inputs.Player.Move.canceled += ctx =>
        {
            m_movement.SetSpeed(0f);
            m_movement.SetRotationSpeed(0f);
        };

        m_inputs.Player.Brake.performed += ctx =>
        {
            m_movement.SetBrakeSpeed(m_statistics.BrakeSpeedMod);
            m_movement.SetBrakeRotationSpeed(m_statistics.BrakeRotationSpeedMod);
        };

        m_inputs.Player.Brake.canceled += ctx =>
        {
            m_movement.SetBrakeSpeed(1f);
            m_movement.SetBrakeRotationSpeed(1f);
        };

        #endregion
    }

    private void OnEnable()
    {
        m_inputs.Enable();
    }

    private void OnDisable()
    {
        m_inputs.Disable();
    }

    void Update()
    {
        m_movement.Move();
    }
}
