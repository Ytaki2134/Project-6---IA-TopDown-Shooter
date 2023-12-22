using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : MonoBehaviour
{
    [SerializeField] private float _damageMake;
    [SerializeField] private float _damageGet;

    public int touch = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (touch == 0)
        {
            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<TankStatistics>().Health -= _damageMake;
            }
            gameObject.GetComponent<TankStatistics>().Health -= _damageGet;

        }
        touch++;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        touch++;
    }
}
