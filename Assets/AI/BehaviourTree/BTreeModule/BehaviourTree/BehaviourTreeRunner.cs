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
    private GameObject[] _waypoints;

    [SerializeField]
    private GameObject _minePrefab;

    Shield shieldComponent;
    [SerializeField]
    private GameObject _WeaponBullet;
    [SerializeField]
    private GameObject[] _summonList;
    private Movement _movement;
    private TankStatistics _tankStat;
    private AudioManager _audioManager;
    private Animator _animatorTrack1;
    private Animator _animatorTrack2;


    // Start is called before the first frame update
    void Start()
    {
        _tankStat = GetComponent<TankStatistics>();


        _movement = GetComponent<Movement>();
        _movement.SetSpeed(_tankStat.Speed);
        _movement.SetRotationSpeed(_tankStat.RotationSpeed);
        shieldComponent = GetComponent<Shield>();
        _audioManager = GetComponent<AudioManager>();
        _animatorTrack1 = GetComponent<Transform>().GetChild(1).GetComponent<Transform>().GetChild(0).GetComponent<Animator>();

        _animatorTrack2 = GetComponent<Transform>().GetChild(1).GetComponent<Transform>().GetChild(1).GetComponent<Animator>();

        shieldComponent.ShieldValue = 20f;
        tree = tree.Clone();
        tree.blackboard.Set("animatorTrack1", _animatorTrack1);
        tree.blackboard.Set("animatorTrack2", _animatorTrack2);
        tree.blackboard.Set("bufferDistance", 3f);

        tree.blackboard.Set("targetGameObject", _targetGameObject);
        tree.blackboard.Set("waypoints", _waypoints);
        tree.blackboard.Set("audioManager", _audioManager);
        tree.blackboard.Set("speed", _tankStat.Speed);
        tree.blackboard.Set("targetEnemi", _targetEnemi);
        tree.blackboard.Set("movement", _movement);
        tree.blackboard.Set("readyToMove", false);
        tree.blackboard.Set("IsInDistance", false);
        tree.blackboard.Set("shield", shieldComponent);
        tree.blackboard.Set("health", _tankStat.Health);
        tree.blackboard.Set("minePrefab", _minePrefab);
        tree.blackboard.Set("angleOffset", 0f);
        tree.blackboard.Set("rotSpeed", 30f);
        tree.blackboard.Set("hasAggro", false);
        tree.blackboard.Set("aggroStartDistance", 40f);
        tree.blackboard.Set("distanceMax", 20f);
        tree.blackboard.Set("distanceMin", 10f);
        tree.blackboard.Set("retreat", false);
        tree.blackboard.Set("retreatDistance", 10f);
        tree.blackboard.Set("checkObstacleCooldown", 20f);


        tree.blackboard.Set("WeaponBullet", _WeaponBullet);
        tree.blackboard.Set("summonList", _summonList);


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

    public GameObject[] GetSummonList()
    {
        return _summonList;
    }
}
