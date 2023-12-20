using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : MonoBehaviour
{
    public bool charge = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Hull>() != null)
        {
            collision.gameObject.GetComponent<Hull>().RemoveHealth(10f);
        }
        gameObject.GetComponent<TankStatistics>().Health -= (2.5f);
    }
}
