using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTankShooting : MonoBehaviour {

    public GameObject shellPrefab;
    public GameObject player;
    public Transform fireTransform;
    public float launchForce = 30f;
    public float shootDelay = 1f;
    private bool canShoot;
    private float shootTimer;

    private void Awake ()
    {
        canShoot = false;
    }
	
	void Update ()
    {
        if (player.activeSelf == false)
        {
            canShoot = false;
        }

        if (canShoot == true)
        {
            shootTimer -= Time.deltaTime;
            if (shootTimer <= 0)
            {
                shootTimer = Time.deltaTime;
                Fire();
            }
        }
    }

    private void Fire()
    {
        GameObject shell = Instantiate(shellPrefab, fireTransform.position, fireTransform.rotation);
        Rigidbody shellRigidbody = shell.GetComponent<Rigidbody>();

        shellRigidbody.velocity = launchForce * fireTransform.forward;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            canShoot = true;
        }
        shootTimer = shootDelay;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            canShoot = false;
        }
    }
}
