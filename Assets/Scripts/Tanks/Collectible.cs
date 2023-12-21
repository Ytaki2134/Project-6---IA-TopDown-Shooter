using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public float healthValue = 20f; // Quantit� de sant� � r�cup�rer

    private void OnTriggerEnter2D(Collider2D other)
    {
        TankStatistics stats = other.gameObject.GetComponentInParent<TankStatistics>();

        // V�rifier si l'objet en collision a un composant Hull
        if (stats != null)
        {
            stats.Health +=(healthValue); // Ajouter de la sant�
            Destroy(gameObject); // D�truire le collectible
        }
    }
}
