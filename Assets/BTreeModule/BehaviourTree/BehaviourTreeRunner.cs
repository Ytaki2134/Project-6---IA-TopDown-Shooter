using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTreeRunner : MonoBehaviour
{
    public BehaviourTree tree;
    [SerializeReference]
    public GameObject _targetGameObject;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private GameObject[] _waypoints;
    // Start is called before the first frame update
    void Start()
    {
       tree = tree.Clone();
        tree.blackboard._targetGameObject = _targetGameObject;
        tree.blackboard._waypoints = _waypoints;
        tree.blackboard._speed = _speed;


    }

    // Update is called once per frame
    void Update()
    {



        tree.Update();

    }
}
