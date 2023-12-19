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

    private Movement  _movement;
    // Start is called before the first frame update
    void Start()
    {
        _movement = GetComponent<Movement>();
        _movement.SetSpeed(_speed);
        _movement.SetRotationSpeed(_speed);

        tree = tree.Clone();
        tree.blackboard.Set("targetGameObject", _targetGameObject);
        tree.blackboard.Set("waypoints", _waypoints);

        tree.blackboard.Set("speed", _speed);
        tree.blackboard.Set("targetEnemi", _targetEnemi);
        tree.blackboard.Set("movement", _movement);
        tree.blackboard.Set("bulletPrefab", _bulletPrefab);
        tree.blackboard.Set("readyToMove", false);
        tree.blackboard.Set("IsInDistance", false);

    }

    // Update is called once per frame
    void Update()
    {
        tree.Update();
    }
}
