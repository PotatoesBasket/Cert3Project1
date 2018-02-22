using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

    public float stopDistance = 8.0f;

    public Transform m_turret;

    private GameObject m_player = null;
    private NavMeshAgent m_navAgent;
    private Rigidbody m_rigid;

    private bool m_follow = false;

    // Use this for initialization
    void Start () {
        //m_player = GameObject.FindGameObjectWithTag("Player");
        m_navAgent = GetComponent<NavMeshAgent>();
        m_rigid = GetComponent<Rigidbody>();
        m_follow = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (m_follow == false)
            return;

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

    private void OnTriggerEnter(Collider other)
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
