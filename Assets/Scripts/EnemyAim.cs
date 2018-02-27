using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAim : MonoBehaviour {

    private GameObject player = null;
    private bool follow;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        follow = false;
    }

    // Update is called once per frame
    void Update () {
        if (follow == true)
        {
            Vector3 lookat = player.transform.position;
            lookat.y = transform.position.y;
            transform.LookAt(lookat);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player") == true)
        {
            follow = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player") == true)
        {
            follow = false;
        }
    }
}
