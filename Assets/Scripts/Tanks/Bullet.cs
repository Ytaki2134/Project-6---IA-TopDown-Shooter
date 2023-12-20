using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D m_rb;
    private Collider2D m_collider;
    private Animator m_animator;
    private GunStatistics m_gunStatistics;

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
        m_rb.AddForce(new Vector2(transform.right.x * m_gunStatistics.BulletSpeed, transform.right.y * m_gunStatistics.BulletSpeed));
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

        //Trigger Animation + animation trigger destroy
        m_animator.SetBool("Hit", true);
        m_rb.constraints = RigidbodyConstraints2D.FreezePosition;
        m_collider.enabled = false;
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
