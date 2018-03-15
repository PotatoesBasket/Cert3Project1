using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float maxLifeTime = 2.0f;
    public float maxDamage = 34.0f;
    public float explosionRadius = 5.0f;
    public float explosionForce = 500f;

    public ParticleSystem explosionParticles;

	void Start ()
    {
        Destroy(gameObject, maxLifeTime);
	}
	
    private void OnCollisionEnter(Collision other)
    {
        Rigidbody targetRigidbody = other.gameObject.GetComponent<Rigidbody>();

        if(targetRigidbody != null)
        {
            targetRigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius);
        }

        TankHealth health = other.gameObject.GetComponent<TankHealth>();
        if (health != null)
        {
            //float damage = CalculateDamage(targetRigidbody.position);
            health.TakeDamage(maxDamage);
        }

        explosionParticles.transform.parent = null;
        explosionParticles.Play();

        Destroy(explosionParticles.gameObject, explosionParticles.main.duration);
        Destroy(gameObject);
    }

    private float CalculateDamage(Vector3 targetPosition) //Returns calculated damage as a float.
    {
        Vector3 explosionToTarget = targetPosition - transform.position;

        float explosionDistance = explosionToTarget.magnitude;
        float relativeDistance = (explosionRadius - explosionDistance) / explosionRadius;
        float damage = relativeDistance * maxDamage;
        damage = Mathf.Max(0f, damage);

        return damage;
    }
}
