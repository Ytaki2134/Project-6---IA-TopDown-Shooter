using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform m_pivot;
    [SerializeField] private Transform m_gunEnd;
    [SerializeField] private float duration;
    private float time = 0;
    private Vector2 m_targetPosition;
    private Quaternion m_targetRotation;
    private GunStatistics m_stats;


    private void Start()
    {
        m_stats = GetComponent<GunStatistics>();
    }

    private void Update()
    {
        FollowTargetPosition();

        if (time > 0)
        {
            time += Time.deltaTime;
        }

        if (time > duration)
        {
            time = 0;
        }
    }

    public void Fire()
    {
        if (time == 0)
        {
            time = Time.deltaTime;
            GameObject temp = Instantiate(m_stats.BulletType, m_gunEnd.position, m_pivot.transform.rotation);
            
            if (m_stats)
            {
                temp.transform.rotation *= Quaternion.Euler(0, 0, 90f);
            }
            temp.GetComponent<Bullet>().SetGunStatsRef(m_stats);
        }
    }

    public void FollowTargetPosition()
    {
        //Rotate Sprite
        var dir = (Vector3) m_targetPosition - transform.position;

        m_targetRotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f);
        m_pivot.rotation = Quaternion.Lerp(transform.rotation, m_targetRotation, Time.deltaTime * m_stats.RotationSpeed * m_stats.BrakeRotationSpeedMod);
    }

    public void SetTargetPosition(Vector2 targetPosition)
    {
        m_targetPosition = targetPosition;
    }
}