using UnityEngine;

public class ActionFireBullet : ActionNode
{
    private float _fireDistance = 100f; // Distance à laquelle le tir est effectué
    private GameObject _targetToMove;
    private GameObject _me;
    public GameObject _bulletPrefab; // Préfabriqué de la balle
    public float bulletSpeed = 10f; // Vitesse de la balle

    protected override void OnStart()
    {
        _targetToMove = blackboard.Get("targetEnemi") as GameObject;
        _me = blackboard.Get("targetGameObject") as GameObject;
        _bulletPrefab = blackboard.Get("bulletPrefab") as GameObject;
    }

    protected override void OnStop()
    {
        // Toute logique de nettoyage nécessaire
    }

    protected override State OnUpdate()
    {
        if (_targetToMove == null)
        {
            return State.Failure; // Échoue si aucune cible n'est définie
        }

        if (Vector2.Distance(_targetToMove.transform.position, _me.transform.position) <= _fireDistance)
        {
            FireBullet();
            return State.Running; // Succès après avoir tiré
        }

        return State.Failure; // Échec si la cible est trop éloignée
    }

    void FireBullet()
    {
        // Créer et lancer la balle
        GameObject bullet = GameObject.Instantiate(_bulletPrefab, _me.transform.position, Quaternion.identity);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

        if (bulletRb != null)
        {
            Vector2 direction = (_targetToMove.transform.position - _me.transform.position).normalized;
            bulletRb.velocity = direction * bulletSpeed;
        }
    }
}
