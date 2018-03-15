using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShooting : MonoBehaviour {

    public GameObject shellPrefab;
    public Transform fireTransform;
    public float launchForce = 30f;
	
	void Update ()
    {
		if (Input.GetButtonDown("Fire1"))
            Fire();
	}

    private void Fire()
    {
        GameObject shell = Instantiate(shellPrefab, fireTransform.position, fireTransform.rotation);
        Rigidbody shellRigidbody = shell.GetComponent<Rigidbody>();

        shellRigidbody.velocity = launchForce * fireTransform.forward;
    }
}
