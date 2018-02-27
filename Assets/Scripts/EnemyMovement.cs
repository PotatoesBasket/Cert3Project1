using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

    public float stopDistance = 8.0f;

    public Transform m_turret;

    private GameObject m_player = null;
    private GameObject m_base = null;
    private NavMeshAgent m_navAgent;

    private bool m_follow = false;

    // Use this for initialization
    void Start () {
        m_base = GameObject.FindGameObjectWithTag("EnemyBase");
        m_navAgent = GetComponent<NavMeshAgent>();
        m_follow = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (m_follow == true)
        {
            float distance = (m_player.transform.position - transform.position).magnitude;

            if (distance > stopDistance)
            {
                m_navAgent.SetDestination(m_player.transform.position);
                m_navAgent.isStopped = false;
            }
            else
            {
                m_navAgent.isStopped = true;
            }
        }

        else
        {
            m_navAgent.SetDestination(m_base.transform.position);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player") == true)
        {
            m_player = other.gameObject;
            m_follow = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player") == true)
        {
            m_follow = false;
        }
    }
}
