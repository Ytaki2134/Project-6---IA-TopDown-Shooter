using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform m_pivot;
    [SerializeField] private Transform m_gunEnd;
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
    }

    public void Fire()
    {
<<<<<<< Updated upstream:Assets/Scripts/Gun.cs
        GameObject temp = Instantiate(m_stats.BulletType, m_gunEnd.position, m_pivot.transform.rotation);
        temp.transform.rotation *= Quaternion.Euler(0, 0, 90f);
        temp.GetComponent<Bullet>().SetGunStatsRef(m_stats);
=======
        GameObject temp;
        switch (m_stats.BulletType.name)
        {
            case "SpreadShot Bullet":
                float angleDif = 120f;
                for (int i = 0; i < 5; i++)
                {
                    temp = Instantiate(m_stats.BulletType, m_gunEnd.position, m_pivot.transform.rotation * Quaternion.Euler(0, 0, angleDif));
                    temp.GetComponent<Bullet>().SetGunStatsRef(m_stats);
                    angleDif -= 15f;
                }
                break;

            case "Homing Bullet":
                break;

            default:
                temp = Instantiate(m_stats.BulletType, m_gunEnd.position, m_pivot.transform.rotation * Quaternion.Euler(0, 0, 90f));
                temp.GetComponent<Bullet>().SetGunStatsRef(m_stats);
                break;
        }


        //Gun Animation
        m_gunAnimator.SetBool("Shoot", true);
        m_audioSource.Play();
>>>>>>> Stashed changes:Assets/Scripts/Tanks/Gun.cs
    }

    private void FollowTargetPosition()
    {
        //Rotate Sprite
<<<<<<< Updated upstream:Assets/Scripts/Gun.cs

        Debug.Log("m_targetPosition : " + m_targetPosition + " ;  transform.position : " + transform.position);

        var dir = (Vector3) m_targetPosition - transform.position;
=======
        var dir = (Vector3)m_targetPosition - transform.position;
>>>>>>> Stashed changes:Assets/Scripts/Tanks/Gun.cs

        Debug.Log("Direction Vector : " + dir.x + " " + dir.y);
        Debug.Log("Atan2 : " + Mathf.Atan2(dir.y, dir.x));

        m_targetRotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f);
        m_pivot.rotation = Quaternion.Lerp(transform.rotation, m_targetRotation, Time.deltaTime * m_stats.RotationSpeed * m_stats.BrakeRotationSpeedMod);
    }

    public void SetTargetPosition(Vector2 targetPosition)
    {
        m_targetPosition = targetPosition;
    }
}
