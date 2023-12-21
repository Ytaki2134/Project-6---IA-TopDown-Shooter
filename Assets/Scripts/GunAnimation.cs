using UnityEngine;

public class GunAnimation : MonoBehaviour
{
    [SerializeField] private Animator m_animator;

    private void EndAnimation()
    {
        m_animator.SetBool("Shoot", false);
    }
}
