using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.NPCCode
{
    public class ConnectedWaypoint : Waypoint
    {
        [SerializeField] protected float _connectivityRadius = 10f;

        List<ConnectedWaypoint> _connections;

        public void Start()
        {
            //grab all waypoint objects in scene
            GameObject[] allWaypoints = GameObject.FindGameObjectsWithTag("Waypoints");

            //Create a list of waypoints i can refer to later
            _connections = new List<ConnectedWaypoint>();

            //check if they're a connected waypoint
            for (int i = 0; i < allWaypoints.Length; i++)
            {
                ConnectedWaypoint nextWaypoint = allWaypoints[i].GetComponent<ConnectedWaypoint>();

                //i.e we found a waypoint
                if (nextWaypoint != null)
                {
                    if (Vector3.Distance(this.transform.position, nextWaypoint.transform.position) <= _connectivityRadius && nextWaypoint != this)
                    {
                        _connections.Add(nextWaypoint);
                    }
                }
            }
        }

        public override void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, debugDrawRadius);

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _connectivityRadius);
        }

        public ConnectedWaypoint NextWaypoint(ConnectedWaypoint previousWaypoint)
        {
            if (_connections.Count == 0)
            {
                //no waypoints ? return null and complain
                Debug.Log("Insufficient waypoint count");
                return null;
            }
            else if (_connections.Count == 1 && _connections.Contains(previousWaypoint))
            {
                //only on waypoint and it's the previous one ? just use taht
                return previousWaypoint;
            }
            else
            {
                //otherwise find a random one that isn't the previous one
                ConnectedWaypoint nextWaypoint;
                int nextIndex = 0;

                do
                {
                    nextIndex = UnityEngine.Random.Range(0, _connections.Count);
                    nextWaypoint = _connections[nextIndex];
                } while (nextWaypoint == previousWaypoint);

                return nextWaypoint;
            }
        }
    }
}