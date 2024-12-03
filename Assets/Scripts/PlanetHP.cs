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
        print("The name of the game object: " + gameObject.name);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(gameObject.name == "Planet1")
        {
            if (other.CompareTag("Fire2"))
            {
                Destroy(other.gameObject); // Destroy the asteroid.
                TakeDamage(); // Example damage value.
            }
        }

        if (gameObject.name == "Planet2")
        {
            if (other.CompareTag("Fire1"))
            {
                Destroy(other.gameObject); // Destroy the asteroid.
                TakeDamage(); // Example damage value.
            }
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
