using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private Camera m_camera;
    private PlayerInputs m_inputs;
    private Rigidbody2D m_rb;
    private TankStatistics m_TankStatistics;
    private Movement m_movement;
    private Gun m_gun;

    void Awake()
    {
        m_inputs = new PlayerInputs();
        m_rb = GetComponent<Rigidbody2D>();
        m_TankStatistics = GetComponent<TankStatistics>();
        m_movement = GetComponentInChildren<Movement>();
        m_gun = GetComponentInChildren<Gun>();

        m_movement.SetSpeed(0f);
        m_movement.SetRotationSpeed(0f);
        m_movement.SetBrakeSpeed(1f);
        m_movement.SetBrakeRotationSpeed(1f);

        #region Input Definitions

        // MOVE

        m_inputs.Player.Move.performed += ctx =>
        {
            m_movement.SetSpeed(m_TankStatistics.Speed);
            m_movement.SetRotationSpeed(m_TankStatistics.RotationSpeed);
            m_movement.SetCurrentMovement(ctx.ReadValue<Vector2>());
        };

        m_inputs.Player.Move.canceled += ctx =>
        {
            m_movement.SetSpeed(0f);
            m_movement.SetRotationSpeed(0f);
        };

        // BRAKE

        m_inputs.Player.Brake.performed += ctx =>
        {
            m_movement.SetBrakeSpeed(m_TankStatistics.BrakeSpeedMod);
            m_movement.SetBrakeRotationSpeed(m_TankStatistics.BrakeRotationSpeedMod);
            m_rb.drag = 5f;
        };

        m_inputs.Player.Brake.canceled += ctx =>
        {
            m_movement.SetBrakeSpeed(1f);
            m_movement.SetBrakeRotationSpeed(1f);
            m_rb.drag = 15f;
        };

        // FIRE

        m_inputs.Player.Fire.performed += ctx =>
        {
            m_gun.Fire();
        };

        #endregion
    }

    void Update()
    {
        m_gun.SetTargetPosition(m_camera.ScreenToWorldPoint(Mouse.current.position.ReadValue()));
    }

    private void FixedUpdate()
    {
        m_movement.Move();
    }

    private void OnEnable()
    {
        m_inputs.Enable();
    }

    private void OnDisable()
    {
        m_inputs.Disable();
    }
}
