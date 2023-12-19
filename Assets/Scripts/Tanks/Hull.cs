using UnityEngine;

public class Hull : MonoBehaviour
{
    [SerializeField] private Transform[] m_transformList = new Transform[2];
    private TankStatistics m_stats;
    private Animator m_animator;
    private Collider2D m_collider;

    void Start()
    {
        m_stats = GetComponentInParent<TankStatistics>();
        m_animator = GetComponent<Animator>();
        m_collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (m_stats.Health <= 0)
        {
            m_animator.SetBool("Dead", true);
            m_collider.enabled = false;
        }
    }

    public void AddHealth(float health)
    {
        m_stats.Health += health;
    }

    public void RemoveHealth(float health)
    {
        m_stats.Health -= health;
    }

    //Animation Functions

    public void ExplodeStart()
    {
        foreach (Transform t in m_transformList) 
        {
            Destroy(t.gameObject);
        }
    }

    public void ExplodeEnd()
    {
        m_animator.SetBool("Dead", false);
        GetComponentInParent<PlayerControls>().DestroyObject();
    }
}
