using UnityEngine;

namespace Assets.Scripts.NPCCode
{
    public class PatrolPoints : MonoBehaviour
    {
        [SerializeField] protected float debugDrawRadius = 1.0f;

        public virtual void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, debugDrawRadius);
        }
    }
}