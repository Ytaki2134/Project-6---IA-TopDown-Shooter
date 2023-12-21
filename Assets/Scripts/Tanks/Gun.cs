using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform m_pivot;
    [SerializeField] private Transform m_gunEnd;
    [SerializeField] private Animator m_shootAnimator;
    private AudioSource m_audioSource;
    private Vector2 m_targetPosition;
    private Quaternion m_targetRotation;
    private GunStatistics m_stats;


    private void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
        m_stats = GetComponent<GunStatistics>();
    }

    private void Update()
    {
        FollowTargetPosition();
    }

    public void Fire()
    {
        GameObject temp;
        switch (m_stats.BulletType.name)
        {
            default:
                temp = Instantiate(m_stats.BulletType, m_gunEnd.position, m_pivot.transform.rotation * Quaternion.Euler(0, 0, 90f));
                temp.GetComponent<Bullet>().SetGunStatsRef(m_stats);
                break;

            case "SpreadShot Bullet":
                float AngleDif = 120f;
                for (int i = 0; i < 5; i++)
                {
                    temp = Instantiate(m_stats.BulletType, m_gunEnd.position, m_pivot.transform.rotation * Quaternion.Euler(0, 0, AngleDif));
                    temp.GetComponent<Bullet>().SetGunStatsRef(m_stats);
                    AngleDif -= 15f;
                }
                break;

            case "Homing Bullet":
                break;

        }
        m_shootAnimator.SetBool("Shoot", true);
        m_audioSource.Play();
    }

    private void FollowTargetPosition()
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
