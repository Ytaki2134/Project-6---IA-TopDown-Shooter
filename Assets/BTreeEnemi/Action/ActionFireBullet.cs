using UnityEngine;

public class ActionFireBullet : ActionNode
{
    private float _fireDistance = 15; // Distance à laquelle le tir est effectué
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

    private float _lastFireTime = 0f; // Dernier moment où une balle a été tirée
    private float _fireInterval = 2f; // Intervalle entre les tirs en secondes


    protected override State OnUpdate()
    {
        if (_targetToMove == null)
        {
            return State.Failure;
        }

        if (Vector2.Distance(_targetToMove.transform.position, _me.transform.position) <= _fireDistance && (bool)blackboard.Get("readyToShoot"))
        {
            if (Time.time - _lastFireTime >= _fireInterval)
            {
                FireBullet();
                _lastFireTime = Time.time; // Mettre à jour le dernier temps de tir
            }
            return State.Running;
        }

        return State.Running;
    }


    void FireBullet()
    {
        // Créer et lancer la balle
        GameObject bullet = Instantiate(_bulletPrefab, _me.GetComponent<Transform>().GetChild(0).GetComponent<Transform>().GetChild(0).GetComponent<Transform>().GetChild(0).GetComponent<Transform>().position, Quaternion.identity);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

        if (bulletRb != null)
        {
            Vector2 direction = (_targetToMove.transform.position - _me.transform.position).normalized;
            bulletRb.velocity = direction * bulletSpeed;

            // Calculer la rotation pour que la balle regarde la cible dès le départ
            Quaternion toRot = Quaternion.Euler(0, 0, Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg);
            bullet.transform.rotation = toRot;
        }
    }

}
