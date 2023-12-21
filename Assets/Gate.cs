using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closeGate : MonoBehaviour
{
    [SerializeField] private GameObject FirstPartGate;
    [SerializeField] private GameObject LastPartGate;
    [SerializeField] private float timeEnterGate = 0;
    private bool active = false;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        active = true;
    }
    private void Update()
    {
        if (active)
        {
            FirstPartGate.SetActive(true);
            if (timeEnterGate < 0)
            {
                LastPartGate.SetActive(true);
                Destroy(gameObject);
            }
            else
                timeEnterGate -= Time.deltaTime;
        }
    }

}
