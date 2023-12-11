using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.Windows;

public class PlayerControls : MonoBehaviour
{
    private Rigidbody2D m_rb;
    private PlayerInputs m_inputs;
    private Vector2 m_currentMovement;

    private Quaternion m_targetRotation;

    [SerializeField] public float Speed = 6f;
    [SerializeField] public float BrakeSpeedMod = 0.5f;
    private float m_speed;
    private float m_brakeSpeedMod;

    [SerializeField] public float RotationSpeed = 2f;
    [SerializeField] public float BrakeRotationSpeedMod = 1.5f;
    private float m_rotationSpeed;
    private float m_brakeRotationSpeedMod;



    void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_inputs = new PlayerInputs();

        m_speed = 0f;
        m_rotationSpeed = 0f;
        m_brakeSpeedMod = 1f;
        m_brakeRotationSpeedMod = 1f;

        #region Input Definitions

        m_inputs.Player.Move.performed += ctx =>
        {
            m_speed = Speed;
            m_rotationSpeed = RotationSpeed;
            m_currentMovement = ctx.ReadValue<Vector2>();
        };

        m_inputs.Player.Move.canceled += ctx =>
        {
            m_speed = 0f;
            m_rotationSpeed = 0f;
        };

        m_inputs.Player.Brake.performed += ctx =>
        {
            m_brakeSpeedMod = BrakeSpeedMod;
            m_brakeRotationSpeedMod = BrakeRotationSpeedMod;
        };

        m_inputs.Player.Brake.canceled += ctx =>
        {
            m_brakeSpeedMod = 1f;
            m_brakeRotationSpeedMod = 1f;
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
        Move();
    }

    private void Move()
    {
        //Rotate Sprite
        m_targetRotation = Quaternion.Euler(0, 0, Mathf.Atan2(-m_currentMovement.x, m_currentMovement.y) * Mathf.Rad2Deg);
        transform.rotation = Quaternion.Lerp(transform.rotation, m_targetRotation, Time.deltaTime * m_rotationSpeed * m_brakeRotationSpeedMod);

        //Move Player (forward only)
        m_rb.AddForce(transform.up * m_speed * m_brakeSpeedMod);
    }
}
