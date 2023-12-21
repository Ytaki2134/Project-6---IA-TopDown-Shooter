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
    private GameObject _WeaponBullet;
    [SerializeField]
    private GameObject _minePrefab;
    [SerializeField]
    private GameObject _bullet2Prefab;
    [SerializeField]
    private GameObject[] _summonList;


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
        tree.blackboard.Set("targetGameObject", gameObject);
        tree.blackboard.Set("waypoints", _waypoints);

        tree.blackboard.Set("speed", _tankStat.Speed);
        tree.blackboard.Set("targetEnemi", _targetEnemi);
        tree.blackboard.Set("movement", _movement);
        tree.blackboard.Set("WeaponBullet", _WeaponBullet);
        tree.blackboard.Set("readyToMove", false);
        tree.blackboard.Set("IsInDistance", false);
        tree.blackboard.Set("shield", shieldComponent);
        tree.blackboard.Set("health", _tankStat.Health);
        tree.blackboard.Set("minePrefab", _minePrefab);
        tree.blackboard.Set("angleOffset", 0f);
        tree.blackboard.Set("rotSpeed", 30f);
        tree.blackboard.Set("hasAggro", false);
        tree.blackboard.Set("aggroStartDistance", 10f);
        tree.blackboard.Set("aggroEndDistance", 20f);
        tree.blackboard.Set("hasBeenHit", false);
        tree.blackboard.Set("Summon", _summonList);
    }

    // Update is called once per frame
    void Update()
    {
        tree.Update();
    }

    public GameObject[] GetSummonList()
    {
        return _summonList;
    }
}
