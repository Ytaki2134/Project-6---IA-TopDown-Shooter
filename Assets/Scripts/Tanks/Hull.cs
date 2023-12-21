using UnityEngine;

public class Hull : MonoBehaviour
{
    private TankStatistics m_stats;

    void Start()
    {
        m_stats = GetComponentInParent<TankStatistics>();
    }

    private void Update()
    {
        if (m_stats.Health <= 0)
        {
            Destroy(m_stats.Parent, 0.2f);
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
}
