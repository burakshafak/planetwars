using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetHP : MonoBehaviour
{
    private Vector3 originalScale;
    private float currentHealth;
    private float damage = 20f;
    public float startingHealth;

    void Start()
    {
        originalScale = transform.localScale;
        print("Original scale of the planet:" + originalScale.x);
        startingHealth = originalScale.x * 100;
        currentHealth = startingHealth;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if hit by an asteroid.
        if (other.CompareTag("Fire"))
        {
            Destroy(other.gameObject); // Destroy the asteroid.
            TakeDamage(); // Example damage value.
        }
    }

    private void TakeDamage()
    {
        currentHealth -= damage;


        // Adjust the planet's size based on health.
        float healthRatio = currentHealth / startingHealth;
        transform.localScale = originalScale * healthRatio;

        if (transform.localScale.x < 0.1)
        {
            // Handle planet destruction.
            Destroy(gameObject);
            return;
        }

    }

    


}
