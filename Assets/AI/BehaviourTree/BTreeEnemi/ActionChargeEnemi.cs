using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ChargeEnemyNode : ActionNode
{
    private GameObject _enemy;
    private GameObject _tank;
    private float _speed;

    protected override void OnStart()
    {
        _enemy = blackboard.Get("targetEnemi") as GameObject;
        _tank = blackboard.Get("targetGameObject") as GameObject;
        _speed = (float)blackboard.Get("speed");
        _speed *= 2; // Doubler la vitesse pour la charge
        blackboard.Set("charge", true);
        _enemy.GetComponent<Charge>().charge = true;
    }

    protected override void OnStop()
    {
        _speed /= 2; // Remettre la vitesse à la normale
        _enemy.GetComponent<Charge>().charge = false;
    }

    protected override State OnUpdate()
    {
        if (_enemy == null || _tank == null)
        {
            return State.Failure;
        }

        // Se diriger directement vers l'ennemi
        Vector2 direction = (_enemy.transform.position - _tank.transform.position).normalized;
        _tank.transform.position += (Vector3)direction * _speed * Time.deltaTime;
        return State.Running;
    }

}
