using System.Collections.Generic;
using UnityEngine;

public class NPCSimplePatrol2D : MonoBehaviour
{
    [SerializeField] bool _patrolWaiting;
    [SerializeField] float _totalWaitTime = 3f;
    [SerializeField] List<Transform> _patrolPoints;

    int _currentPatrolIndex;
    bool _traveling;
    bool _waiting;
    float _waitTimer;

    private Movement m_movement;

    void Awake()
    {
        m_movement = GetComponentInChildren<Movement>();

        m_movement.SetSpeed(3f);
        m_movement.SetRotationSpeed(2f);
        m_movement.SetBrakeSpeed(1f);
        m_movement.SetBrakeRotationSpeed(1f);
    }

    public void Start()
    {
        if (_patrolPoints != null && _patrolPoints.Count >= 2)
        {
            _currentPatrolIndex = 0;
            SetDestination();
        }
        else
        {
            Debug.Log("Insufficient patrol points for basic patrolling behavior");
        }

        if (m_movement == null)
        {
            Debug.LogError("Movement component not found!");
        }
    }

    public void Update()
    {
        if (_traveling)
        {
            float distance = Vector2.Distance(transform.position, _patrolPoints[_currentPatrolIndex].position);
            if (distance <= 0.1f)
            {
                _traveling = false;

                if (_patrolWaiting)
                {
                    _waiting = true;
                    _waitTimer = 0f;
                }
                else
                {
                    ChangePatrolPoint();
                    SetDestination();
                }
            }
            else
            {
                if (m_movement != null)
                {
                    m_movement.SetCurrentMovement(((Vector2)_patrolPoints[_currentPatrolIndex].position - (Vector2)transform.position).normalized);
                    m_movement.Move();
                }
            }
        }

        if (_waiting)
        {
            _waitTimer += Time.deltaTime;
            if (_waitTimer >= _totalWaitTime)
            {
                _waiting = false;

                ChangePatrolPoint();
                SetDestination();
            }
        }
    }

    private void SetDestination()
    {
        if (_patrolPoints != null)
        {
            _traveling = true;
        }
    }

    private void ChangePatrolPoint()
    {
        _currentPatrolIndex++;
        
        if (_currentPatrolIndex >= _patrolPoints.Count)
        {
            _currentPatrolIndex = 0;
        }
    }
}
