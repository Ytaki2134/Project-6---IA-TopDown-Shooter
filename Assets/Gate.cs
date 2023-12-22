using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class closeGate : MonoBehaviour
{
    [SerializeField] private GameObject FirstPartGate;
    [SerializeField] private GameObject LastPartGate;
    [SerializeField] private GameObject player;
    [SerializeField] private float timeEnterGate = 0;
    [SerializeField] private bool goNextLevel = false;

    private bool active = false;

    private void Start()
    {
        if (!goNextLevel) 
        {
            player.GetComponent<PlayerControls>().enabled = false;
            player.GetComponent<GoUp>().enabled = true;
        }
    }

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInChildren<Hull>() != null)
        {
            active = true;
            if(!goNextLevel)
            {
                player.GetComponent<PlayerControls>().enabled = true;
                player.GetComponent<GoUp>().enabled = false;
            }
        }
    }
    private void Update()
    {
        if (active)
        {
            FirstPartGate.SetActive(true);
            if (timeEnterGate < 0)
            {
                LastPartGate.SetActive(true);
                if (goNextLevel)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
                }

                else
                    Destroy(gameObject);

            }
            else
                timeEnterGate -= Time.deltaTime;
        }
    }

    
}
