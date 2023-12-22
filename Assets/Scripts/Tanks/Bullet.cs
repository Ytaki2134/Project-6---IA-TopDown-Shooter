using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D m_rb;
    private Collider2D m_collider;
    private Animator m_animator;
    private GunStatistics m_gunStatistics;

    private Transform m_playerTransform;
    private Vector3 m_direction;
    private Quaternion m_targetRotation;
    public GameObject hitEffectPrefab;
    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_collider = GetComponent<Collider2D>();
        m_animator = GetComponent<Animator>();

        switch (m_gunStatistics.IsPlayer)
        {
            case true:
                gameObject.tag = "PlayerBullet";
                break;

            case false:
                gameObject.tag = "EnemyBullet";
                break;
        }

        switch (m_gunStatistics.BulletType.name)
        {
            default:
                break;

            case "Missile Bullet":
                m_playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_gunStatistics.BulletType.name)
        {
            default:
                m_rb.AddForce(new Vector3(transform.right.x * m_gunStatistics.BulletSpeed, transform.right.y * m_gunStatistics.BulletSpeed));
                break;

            case "Missile Bullet":
                //Move toward player
                m_direction = m_playerTransform.position - transform.position;
                m_rb.AddForce(new Vector2(m_direction.normalized.x * m_gunStatistics.BulletSpeed, m_direction.normalized.y * m_gunStatistics.BulletSpeed));

                //Rotate toward Player
                m_targetRotation = Quaternion.Euler(0, 0, Mathf.Atan2(m_direction.y, m_direction.x) * Mathf.Rad2Deg);
                transform.rotation = Quaternion.Lerp(transform.rotation, m_targetRotation, Time.deltaTime * 50f);
                break;

            case "Flame Bullet":

                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Don't kill allies checks
        if (gameObject.tag == "PlayerBullet" && collision.gameObject.layer == 6 || gameObject.tag == "EnemyBullet" && collision.gameObject.layer == 7)
            return;

        //Don't kill allied bullets
        if (gameObject.tag == collision.gameObject.tag)
            return;


        if (collision.gameObject.GetComponent<Hull>() != null)
        {
            collision.gameObject.GetComponent<Hull>().RemoveHealth(m_gunStatistics.BulletDamage);
        }

        switch (m_gunStatistics.BulletType.name)
        {
            default:
                //Trigger Animation + animation trigger destroy
                m_animator.SetBool("Hit", true);
                m_rb.constraints = RigidbodyConstraints2D.FreezePosition;
                m_collider.enabled = false;
                break;

            case "Flame Bullet":

                break;
        }

        if (gameObject.tag == "EnemyBossBullet")
        {
            InstantiateHitEffect(collision);


        }


    }

    //Should only work for flame bullets as other bullets destroy themselves before this function
    private void OnTriggerStay2D(Collider2D collision)
    {
        //Don't kill allies checks
        if (gameObject.tag == "PlayerBullet" && collision.gameObject.layer == 6 || gameObject.tag == "EnemyBullet" && collision.gameObject.layer == 7)
            return;

        //Don't kill allied bullets
        if (gameObject.tag == collision.gameObject.tag)
            return;


        if (collision.gameObject.GetComponent<Hull>() != null)
        {
            collision.gameObject.GetComponent<Hull>().RemoveHealth(m_gunStatistics.BulletDamage);
        }
    }
    private void InstantiateHitEffect(Collider2D collision)
    {
        if (hitEffectPrefab != null)
        {
            // Calculer le point le plus proche sur le collider du tank
            Vector2 closestPoint = collision.ClosestPoint(transform.position);

            // Instancier l'effet à ce point
            GameObject effect = Instantiate(hitEffectPrefab, closestPoint, Quaternion.identity);

            // Ajuster le temps de destruction pour correspondre à la durée de l'animation
            float effectDuration = 0.2f; // Remplacer par la durée réelle de l'animation
            Destroy(effect, effectDuration);
        }
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }

    public void SetGunStatsRef(GunStatistics gunStatistics)
    {
        m_gunStatistics = gunStatistics;
    }

}
