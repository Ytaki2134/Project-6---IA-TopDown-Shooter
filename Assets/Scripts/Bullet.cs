using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D m_rb;
    private GunStatistics m_gunStatistics;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
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

        Destroy(gameObject);
    }

    public void SetGunStatsRef(GunStatistics gunStatistics)
    {
        m_gunStatistics = gunStatistics;
    }
}
