using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTankShooting : MonoBehaviour {

    public GameObject shellPrefab;
    public Transform fireTransform;
    public float launchForce = 30f;
    public float shootDelay = 1f;
    private bool canShoot;
    private float shootTimer;

    private void Awake()
    {
        canShoot = false;
    }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
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
