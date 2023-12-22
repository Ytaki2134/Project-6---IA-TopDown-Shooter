using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    [SerializeField] private GameObject transition;
    [SerializeField] private GameObject Tank;

    private bool activenext = false;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.name = "Transition";
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position =new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.1f);
        if((int)Tank.transform.position.y == (int)gameObject.transform.position.y-1 && !activenext)
        {
            Instantiate(transition, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + (12 * 2.56f)), Quaternion.identity);
            activenext = true;
        }
        if ((int)(Tank.transform.position.y) > (int)gameObject.transform.position.y + (12 * 2.56f))
            Destroy(gameObject);
    }
}
