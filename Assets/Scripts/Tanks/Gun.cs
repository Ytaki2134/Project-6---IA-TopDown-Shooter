using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform m_pivot;
    [SerializeField] private Transform m_gunEnd;
    private Vector2 m_targetPosition = new(0, 0);
    private Quaternion m_targetRotation;
    private GunStatistics m_stats;
    private AudioManager m_audioManager;
    [SerializeField] private float duration;
    private float time = 0;
    private void Start()
    {
        m_stats = GetComponent<GunStatistics>();
        m_audioManager = GetComponent<AudioManager>();
    }

    private void Update()
    {
        if (m_targetPosition != new Vector2(0, 0))
            FollowTargetPosition();
    }

    public void Fire()
    {
        if (time == 0)
        {
            time = Time.deltaTime;
            GameObject temp = Instantiate(m_stats.BulletType, m_gunEnd.position, m_pivot.transform.rotation);
            if (m_stats.IsPlayer)
            {
                temp.transform.rotation *= Quaternion.Euler(0, 0, 90f);

            }
            temp.GetComponent<Bullet>().SetGunStatsRef(m_stats);
            m_audioManager.PlaySound();

        }
        else
        {
            time = (time > duration ? time = 0 : time += Time.deltaTime);
        }
    }

    private void FollowTargetPosition()
    {
        //Rotate Sprite
        var dir = (Vector3)m_targetPosition - transform.position;
        m_targetRotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f);
        m_pivot.rotation = Quaternion.Lerp(transform.rotation, m_targetRotation, Time.deltaTime * m_stats.RotationSpeed * m_stats.BrakeRotationSpeedMod);
    }

    public void SetTargetPosition(Vector2 targetPosition)
    {
        m_targetPosition = targetPosition;
    }
}
