using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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
    [SerializeField]
    private GameObject _bullet2Prefab;


    private Movement  _movement;
    private TankStatistics  _tankStat;

    // Start is called before the first frame update
    void Start()
    {
        _tankStat = GetComponent<TankStatistics>();
        

        _movement = GetComponent<Movement>();
        _movement.SetSpeed(_tankStat.Speed);
        _movement.SetRotationSpeed(_tankStat.RotationSpeed);
        Shield shieldComponent = GetComponent<Shield>();

        tree = tree.Clone();
        tree.blackboard.Set("targetGameObject", _targetGameObject);
        tree.blackboard.Set("waypoints", _waypoints);

        tree.blackboard.Set("speed", _tankStat.Speed);
        tree.blackboard.Set("targetEnemi", _targetEnemi);
        tree.blackboard.Set("movement", _movement);
        tree.blackboard.Set("bulletPrefab", _bulletPrefab);
        tree.blackboard.Set("readyToMove", false);
        tree.blackboard.Set("IsInDistance", false);
        tree.blackboard.Set("shield", shieldComponent);
        tree.blackboard.Set("health", _tankStat.Health);
        tree.blackboard.Set("minePrefab", _minePrefab);
        tree.blackboard.Set("angleOffset", 0f);
        tree.blackboard.Set("rotSpeed", 30f);
        tree.blackboard.Set("hasAggro", false);
        tree.blackboard.Set("aggroStartDistance", 40f);
        tree.blackboard.Set("tooNearFromBoss", false);

        tree.blackboard.Set("aggroEndDistance", 200f);
        tree.blackboard.Set("hasBeenHit", false);







        tree.blackboard.Set("isNearObstacle", false);
        tree.blackboard.Set("hasChosenAvoidanceDirection", false);
        tree.blackboard.Set("isObstacleDetected", false);
        tree.blackboard.Set("lastAvoidanceTime", 0f);



    }

    // Update is called once per frame
    void Update()
    {
        tree.Update();
    }
}
