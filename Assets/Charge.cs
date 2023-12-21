using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : MonoBehaviour
{
    [SerializeField] private float _damageMake;
    [SerializeField] private float _damageGet;

    public bool touch = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (touch == true)
        {
            if (collision.gameObject.GetComponent<TankStatistics>() != null)
            {
                collision.gameObject.GetComponent<TankStatistics>().Health -= _damageMake;
            }
            gameObject.GetComponent<TankStatistics>().Health -= _damageGet;
            touch = false;
        }
    }
}
