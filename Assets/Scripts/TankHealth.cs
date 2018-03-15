using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankHealth : MonoBehaviour {

    public float startHealth = 100f;
    private float currentHealth;
    private bool isDead = false;

    public GameObject explosionPrefab;
    private ParticleSystem explosionParticles;

    private void Awake()
    {
        Initialise();
    }

    public void Initialise() //Resets tank health, sets isDead false, deactivates explosion.
    {
        currentHealth = startHealth;
        isDead = false;

        explosionParticles = Instantiate(explosionPrefab).GetComponent<ParticleSystem>();
        explosionParticles.gameObject.SetActive(false);
    }

    public void TakeDamage(float amount) //Minuses damage from currentHealth, checks if tank should be dead.
    {
        currentHealth -= amount;

        if (currentHealth <= 0 && !isDead)
        {
            OnDeath();
        }
    }

    private void OnDeath() //Sets isDead true, makes an explosion, deactivates tank.
    {
        isDead = true;

        explosionParticles.transform.position = transform.position;
        explosionParticles.gameObject.SetActive(true);
        explosionParticles.Play();

        gameObject.SetActive(false);
    }
}
