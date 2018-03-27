using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

    public float stopDistance = 8.0f;
    private bool follow = false;

    public Transform turret;
    private GameObject player = null;
    private NavMeshAgent navAgent;

    public GameObject[] waypoints;
    private int currentWaypoint = 0;

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        follow = false;
	}
	
	void Update()
    {
        if (player != null)
        {
            if (player.activeSelf == false)
                follow = false;
        }

        if (follow == true)
        {
            float distance = (player.transform.position - transform.position).magnitude;

            if (distance > stopDistance)
            {
                navAgent.SetDestination(player.transform.position);
                navAgent.isStopped = false;
            }
            else
            {
                navAgent.isStopped = true;
            }

            if (turret != null)
            {
                turret.LookAt(player.transform);
            }
        }
        else
        {
            if(navAgent.remainingDistance < 2f)
            {
                if(waypoints != null)
                {
                    currentWaypoint += 1;
                    if(currentWaypoint >= waypoints.Length)
                    {
                        currentWaypoint = 0;
                    }

                    if(waypoints[currentWaypoint] != null)
                    {
                        GameObject nextWaypoint = waypoints[currentWaypoint];
                        navAgent.SetDestination(nextWaypoint.transform.position);
                    }
                }
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player") == true)
        {
            player = other.gameObject;
            follow = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag.Equals("Player") == true)
        {
            follow = false;
            navAgent.isStopped = false;

            if(waypoints != null && waypoints[currentWaypoint] != null)
            {
                GameObject nextWaypoint = waypoints[currentWaypoint];
                navAgent.SetDestination(nextWaypoint.transform.position);
            }
        }
    }
}
