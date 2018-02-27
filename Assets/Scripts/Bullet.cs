using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float maxLifeTime = 2.0f;
    public float maxDamage = 34.0f;
    public float explosionRadius = 5.0f;
    public float explosionForce = 100f;

    public ParticleSystem explosionParticles;

	void Start () {
        Destroy(gameObject, maxLifeTime);
	}
	
    private void OnCollisionEnter(Collision other)
    {
        Rigidbody targetRigidbody = other.gameObject.GetComponent<Rigidbody>();

        if(targetRigidbody != null)
        {
            //do later i guess
        }

        TankHealth health = other.gameObject.GetComponent<TankHealth>();
        if (health!= null)
        {
            health.TakeDamage(maxDamage);
        }

        explosionParticles.transform.parent = null;
        explosionParticles.Play();

        Destroy(explosionParticles.gameObject, explosionParticles.main.duration);
        Destroy(gameObject);
    }
}
