using Cinemachine;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private Camera m_camera;
    [SerializeField] private CinemachineVirtualCamera m_vCamera;
    [SerializeField] private float m_maxZoom = 18f;
    [SerializeField] private float m_minZoom = 8f;
    [SerializeField] private AudioSource m_trackAudioSource;
    [SerializeField] private Animator m_hullAnimator;
    [SerializeField] private Animator m_gunAnimator;
    [SerializeField] private Animator m_shootAnimator;
    [SerializeField] private Animator[] m_trackAnimator = new Animator[2];
    private PlayerInputs m_inputs;
    private Rigidbody2D m_rb;
    private TankStatistics m_tankStatistics;
    private Movement m_movement;
    private Gun m_gun;
    //Animation Pallet Index : 1 = Standard, 2 = SpreadShot, 3 = Sniper, 4 = Missile, 5 = lance-flamme?
    public int type = 0;

    void Awake()
    {
        m_inputs = new PlayerInputs();
        m_rb = GetComponent<Rigidbody2D>();
        m_tankStatistics = GetComponent<TankStatistics>();
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
            m_movement.SetSpeed(m_tankStatistics.Speed);
            m_movement.SetRotationSpeed(m_tankStatistics.RotationSpeed);
            m_movement.SetCurrentMovement(ctx.ReadValue<Vector2>());

            m_movement.SetMovementVolume(0.6f);
            m_trackAudioSource.Play();

            //will be moved to movement script
            m_trackAnimator[0].SetBool("Enable", true);
            m_trackAnimator[1].SetBool("Enable", true);
        };
        m_inputs.Player.Move.canceled += ctx =>
        {
            m_movement.SetSpeed(0f);
            m_movement.SetRotationSpeed(0f);

            m_movement.SetMovementVolume(0.4f);
            m_trackAudioSource.Stop();

            //will be moved to movement script
            m_trackAnimator[0].SetBool("Enable", false);
            m_trackAnimator[1].SetBool("Enable", false);
        };

        // BRAKE

        m_inputs.Player.Brake.performed += ctx =>
        {
            m_movement.SetBrakeSpeed(m_tankStatistics.BrakeSpeedMod);
            m_movement.SetBrakeRotationSpeed(m_tankStatistics.BrakeRotationSpeedMod);
            m_rb.drag = 5f;
        };

        m_inputs.Player.Brake.canceled += ctx =>
        {
            m_movement.SetBrakeSpeed(1f);
            m_movement.SetBrakeRotationSpeed(1f);
            m_rb.drag = 15f;
        };

        // FIRE

        m_inputs.Player.Fire.started += ctx =>
        {
            m_gun.Fire();
        };

        // ZOOM

        m_inputs.Player.Zoom.performed += ctx =>
        {
            if (ctx.ReadValue<Vector2>().normalized.y < 0f && m_vCamera.m_Lens.OrthographicSize <= m_minZoom || ctx.ReadValue<Vector2>().normalized.y > 0f && m_vCamera.m_Lens.OrthographicSize >= m_maxZoom)
            {
                return;
            }

            m_vCamera.m_Lens.OrthographicSize += ctx.ReadValue<Vector2>().normalized.y;
        };

        #endregion
    }

    private void Start()
    {
        m_hullAnimator.SetInteger("Index", type);
        m_gunAnimator.SetInteger("Index", type);
        m_shootAnimator.SetInteger("Index", type);
        m_trackAnimator[0].SetInteger("Index", type);
        m_trackAnimator[1].SetInteger("Index", type);
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

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
