using Cinemachine;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private Camera m_camera;
    [SerializeField] private CinemachineVirtualCamera m_Vcamera;
    [SerializeField] private float m_MaxZoom = 18f;
    [SerializeField] private float m_MinZoom = 8f;
    [SerializeField] private Animator[] m_TrackAnimator = new Animator[2];
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

        GetComponentInChildren<GunStatistics>().IsPlayer = true;

        #region Input Definitions

        // MOVE

        m_inputs.Player.Move.performed += ctx =>
        {
            m_movement.SetSpeed(m_TankStatistics.Speed);
            m_movement.SetRotationSpeed(m_TankStatistics.RotationSpeed);
            m_movement.SetCurrentMovement(ctx.ReadValue<Vector2>());

            m_TrackAnimator[0].SetBool("Enable", true);
            m_TrackAnimator[1].SetBool("Enable", true);

        };
        m_inputs.Player.Move.canceled += ctx =>
        {
            m_movement.SetSpeed(0f);
            m_movement.SetRotationSpeed(0f);
            m_TrackAnimator[0].SetBool("Enable", false);
            m_TrackAnimator[1].SetBool("Enable", false);
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

        // ZOOM

        m_inputs.Player.Zoom.performed += ctx =>
        {
            if (ctx.ReadValue<Vector2>().normalized.y < 0f && m_Vcamera.m_Lens.OrthographicSize <= m_MinZoom || ctx.ReadValue<Vector2>().normalized.y > 0f && m_Vcamera.m_Lens.OrthographicSize >= m_MaxZoom)
            {
                return;
            }

            m_Vcamera.m_Lens.OrthographicSize += ctx.ReadValue<Vector2>().normalized.y;
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
