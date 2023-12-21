using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemi : MonoBehaviour
{
    private Rigidbody2D m_rb;
    private GunStatistics m_gunStatistics;
    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        gameObject.tag = "EnemyBullet";
    }
    public void SetStatistic(GunStatistics newStatistics)
    {
        m_gunStatistics = newStatistics;
    } 
    // Update is called once per frame
    void Update()
    {
        m_rb.AddForce(new Vector2(transform.right.x * m_gunStatistics.BulletSpeed, transform.right.y * m_gunStatistics.BulletSpeed));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Assurez-vous que l'ennemi a le tag "Ennemi"
        {
            Debug.Log("Touché");
            collision.gameObject.GetComponent<Hull>().RemoveHealth(m_gunStatistics.BulletDamage);
            // Ajoutez ici toute autre logique (comme infliger des dégâts à l'ennemi)
        }
        Destroy(gameObject); // Détruire la balle
    }
}