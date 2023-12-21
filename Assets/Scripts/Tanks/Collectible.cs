using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public float healthValue = 20f; // Quantité de santé à récupérer

    private void OnTriggerEnter2D(Collider2D other)
    {
        TankStatistics stats = other.gameObject.GetComponentInParent<TankStatistics>();

        // Vérifier si l'objet en collision a un composant Hull
        if (stats != null)
        {
            stats.Health +=(healthValue); // Ajouter de la santé
            Destroy(gameObject); // Détruire le collectible
        }
    }
}
