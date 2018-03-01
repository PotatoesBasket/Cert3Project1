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

    private void Initialise()
    {
        currentHealth = startHealth;
        isDead = false;

        explosionParticles = Instantiate(explosionPrefab).GetComponent<ParticleSystem>();
        explosionParticles.gameObject.SetActive(false);
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0 && !isDead)
        {
            OnDeath();
        }
    }

    private void OnDeath()
    {
        isDead = true;

        explosionParticles.transform.position = transform.position;
        explosionParticles.gameObject.SetActive(true);
        explosionParticles.Play();

        gameObject.SetActive(false);

        if (tag != "Player")
        {
            GameObject spawnPoint = GameObject.Find("EnemyBase");

            if (spawnPoint != null)
            {
                transform.position = spawnPoint.transform.position;
                Initialise();
                gameObject.SetActive(true);
            }
        }
    }
}
