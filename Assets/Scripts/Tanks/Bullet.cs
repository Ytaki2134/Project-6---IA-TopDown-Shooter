using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D m_rb;
    private Collider2D m_collider;
    private Animator m_animator;
    private GunStatistics m_gunStatistics;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.tag == "PlayerBullet")
        {
            m_rb.AddForce(new Vector2(transform.right.x * m_gunStatistics.BulletSpeed, transform.right.y * m_gunStatistics.BulletSpeed));
        }
        else
        {
            m_rb.AddForce(new Vector2(transform.up.x * m_gunStatistics.BulletSpeed, transform.up.y * m_gunStatistics.BulletSpeed));
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
            collision.gameObject.GetComponent<Hull>().RemoveHealth(m_gunStatistics.BulletDamage);

        if (collision.gameObject.GetComponent<TankStatistics>() != null)
            collision.gameObject.GetComponent<TankStatistics>().Health-= m_gunStatistics.BulletDamage;

        //Trigger Animation + animation trigger destroy
        if (gameObject.tag == "PlayerBullet")
        {
            m_animator.SetBool("Hit", true);
            m_rb.constraints = RigidbodyConstraints2D.FreezePosition;
            m_collider.enabled = false;

        }
        if (gameObject.tag == "EnemyBullet")
        {
        InstantiateHitEffect(collision);


        }

        DestroyBullet();
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
