using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemi : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Assurez-vous que l'ennemi a le tag "Ennemi"
        {
            Debug.Log("Touch�");
            Destroy(gameObject); // D�truire la balle
            // Ajoutez ici toute autre logique (comme infliger des d�g�ts � l'ennemi)
        }
    }
}