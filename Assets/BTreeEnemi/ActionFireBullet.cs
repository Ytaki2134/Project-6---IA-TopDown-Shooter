using UnityEngine;

public class ActionFireBullet : ActionNode
{
    private float _fireDistance = 100f; // Distance � laquelle le tir est effectu�
    private GameObject _targetToMove;
    private GameObject _me;
    public GameObject _bulletPrefab; // Pr�fabriqu� de la balle
    public float bulletSpeed = 10f; // Vitesse de la balle

    protected override void OnStart()
    {
        _targetToMove = blackboard.Get("targetEnemi") as GameObject;
        _me = blackboard.Get("targetGameObject") as GameObject;
        _bulletPrefab = blackboard.Get("bulletPrefab") as GameObject;
    }

    protected override void OnStop()
    {
        // Toute logique de nettoyage n�cessaire
    }

    protected override State OnUpdate()
    {
        if (_targetToMove == null)
        {
            return State.Failure; // �choue si aucune cible n'est d�finie
        }

        if (Vector2.Distance(_targetToMove.transform.position, _me.transform.position) <= _fireDistance)
        {
            FireBullet();
            return State.Running; // Succ�s apr�s avoir tir�
        }

        return State.Failure; // �chec si la cible est trop �loign�e
    }

    void FireBullet()
    {
        // Cr�er et lancer la balle
        GameObject bullet = GameObject.Instantiate(_bulletPrefab, _me.transform.position, Quaternion.identity);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

        if (bulletRb != null)
        {
            Vector2 direction = (_targetToMove.transform.position - _me.transform.position).normalized;
            bulletRb.velocity = direction * bulletSpeed;
        }
    }
}
