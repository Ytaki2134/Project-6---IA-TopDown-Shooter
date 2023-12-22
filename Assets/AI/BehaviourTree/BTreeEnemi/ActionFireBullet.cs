using UnityEngine;

public class ActionFireBullet : ActionNode
{
    private float _fireDistance = 15; // Distance à laquelle le tir est effectué
    private GameObject _targetToMove;
    private GameObject _me;
    private GameObject _canon;

    public float bulletSpeed = 10f; // Vitesse de la balle

    protected override void OnStart()
    {
        _targetToMove = blackboard.Get("targetEnemi") as GameObject;
        _me = blackboard.Get("targetGameObject") as GameObject;
        _canon = blackboard.Get("WeaponBullet") as GameObject;
    }

    protected override void OnStop()
    {
        // Toute logique de nettoyage nécessaire
    }

    private float _lastFireTime = 0f; // Dernier moment où une balle a été tirée
    private float _fireInterval = 1f; // Intervalle entre les tirs en secondes


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
                //Debug.Log(_audioEnemi != null);
                _lastFireTime = Time.time; // Mettre à jour le dernier temps de tir
            }
            return State.Running;
        }

        return State.Failure;
    }


    void FireBullet()
    {
        _canon.GetComponent<Gun>().SetTargetPosition(_targetToMove.transform.position);
        _canon.GetComponent<Gun>().Fire();
        /*GameObject temp = Instantiate(_canon.GetComponent<GunStatistics>().BulletType, _me.transform.position, _canon.GetComponent<Transform>().transform.rotation);
        temp.GetComponent<Bullet>().SetGunStatsRef(_canon.GetComponent<GunStatistics>());*/
    }

}
