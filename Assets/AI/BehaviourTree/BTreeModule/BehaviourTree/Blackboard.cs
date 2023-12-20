using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
//public class Blackboard
//{
//    [SerializeReference]
//    public GameObject _targetGameObject;
//    [SerializeField]
//    public  float _speed;
//    [SerializeField]
//    public GameObject[] _waypoints;
//    public GameObject _targetToMove;
//    public  Movement _movement;
//
//}


public class Blackboard
{
    private Dictionary<string, object> data = new Dictionary<string, object>();

    public void Set(string key, object value)
    {
        data[key] = value;
    }

    public object Get(string key)
    {
        object value;
        if (data.TryGetValue(key, out value))
        {
            return value;
        }

        throw new ArgumentException("Key not found in blackboard: " + key);
    }

    public T Get<T>(string key)
    {
        return (T)Get(key);
    }

    public bool TryGet<T>(string key, out T value)
    {
        object objValue;
        if (data.TryGetValue(key, out objValue) && objValue is T)
        {
            value = (T)objValue;
            return true;
        }

        value = default(T);
        return false;
    }

    public bool ContainsKey(string key)
    {
        return data.ContainsKey(key);
    }

    public void Remove(string key)
    {
        data.Remove(key);
    }

    public void Clear()
    {
        data.Clear();
    }
}
