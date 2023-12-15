using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Blackboard
{
    [SerializeReference]
    public GameObject _targetGameObject;
    [SerializeField]
    public  float _speed;
    [SerializeField]
    public GameObject[] _waypoints;
    public GameObject _targetToMove;

}
