using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTreeRunner : MonoBehaviour
{
    public BehaviourTree tree;
    [SerializeReference]
    public GameObject _targetGameObject;
    [SerializeReference]
    public GameObject _targetEnemi;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private GameObject[] _waypoints;
    [SerializeField]
    private GameObject _bulletPrefab;
    [SerializeField]
    private GameObject _minePrefab;

    private Movement  _movement;
    Shield shield;

    // Start is called before the first frame update
    void Start()
    {
        TankStatistics tankStatistics = GetComponent<TankStatistics>();
        _movement = GetComponent<Movement>();
        _movement.SetSpeed(_speed);
        _movement.SetRotationSpeed(_speed);
        shield = GetComponent<Shield>();
        shield.ShieldValue = 50;


        tree = tree.Clone();
        tree.blackboard.Set("targetGameObject", _targetGameObject);
        tree.blackboard.Set("waypoints", _waypoints);

        tree.blackboard.Set("speed", _speed);
        tree.blackboard.Set("targetEnemi", _targetEnemi);
        tree.blackboard.Set("movement", _movement);
        tree.blackboard.Set("bulletPrefab", _bulletPrefab);
        tree.blackboard.Set("readyToMove", false);
        tree.blackboard.Set("readyToShoot", false);
        tree.blackboard.Set("IsInDistance", false);
        tree.blackboard.Set("health", tankStatistics.Health);
        tree.blackboard.Set("canUsePatternOne", true);
        tree.blackboard.Set("minePrefab", _minePrefab);
        tree.blackboard.Set("shield", shield);






    }

    // Update is called once per frame
    void Update()
    {



        tree.Update();

    }
}
