using UnityEngine;
using UnityEngine.AI;

public class NPCMove : MonoBehaviour
{
    [SerializeField] Transform _destination;

    NavMeshAgent _navMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();

        if ( _navMeshAgent == null)
        {
            Debug.LogError("the nav mesh agent component is not attached to " + gameObject.name);
        }
        else
        {
            SetDestination();
        }
    }

    private void SetDestination()
    {
        if ( _destination != null )
        {
            Vector3 targetVector = _destination.transform.position;
            _navMeshAgent.SetDestination( targetVector );
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
