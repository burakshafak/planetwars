using UnityEngine;

using System.Collections;
using System.Collections.Generic;


public class PlanetHP : MonoBehaviour
{
    private Vector3 originalScale;
    private float currentHealth;
    private float damage = 10f;
    public float startingHealth;

    public GameManager gameManager;

    [SerializeField] private  float starPower = 2.5f;
    [SerializeField] private float astreoidDamage = 12.5f;

    private Vector3 damageRate;

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

        bool isfire = true;
        if (gameObject.name == "Planet1")
        {
            if (other.CompareTag("Fire2"))
            {
                Destroy(other.gameObject); // Destroy the asteroid.
                TakeFireDamage(isfire); // Example damage value.
            }
        }

        if (gameObject.name == "Planet2")
        {
            if (other.CompareTag("Fire1"))
            {
                Destroy(other.gameObject); // Destroy the asteroid.
                TakeFireDamage(isfire); // Example damage value.
            }
        }

        if (other.CompareTag("Star"))
        {
            print("Collected a star.");
            heal();
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Astreoid"))
        {
            damageRate = other.transform.localScale;
            isfire = false;
            print("Collided with an astreoid.");
            TakeAstreoidDamage(damageRate);
            Destroy(other.gameObject);  

        }


    }

    private void TakeFireDamage(bool fire)
    {
        if (fire)
        {
            currentHealth -= damage;
        }
        else if (!fire)
        {
            currentHealth -= astreoidDamage;
        }



        // Adjust the planet's size based on health.
        float healthRatio = currentHealth / startingHealth;
        transform.localScale = originalScale * healthRatio;

        if (transform.localScale.x < 0.1)
        {
            // Handle planet destruction.

            if (gameObject.name == "Planet1")
            {
                print("Player1 died");
                gameManager.PlayerDied(1);
            }
            else if (gameObject.name == "Planet2")
            {
                print("Player2 died");
                gameManager.PlayerDied(2);
            }
            return;
        }

    }

    private void TakeAstreoidDamage(Vector3 damageRatee)
    {

        currentHealth = currentHealth - (astreoidDamage * damageRatee.x);

        // Adjust the planet's size based on health.
        float healthRatio = currentHealth / startingHealth;
        transform.localScale = originalScale * healthRatio;

        if (transform.localScale.x < 0.1)
        {
            // Handle planet destruction.

            if (gameObject.name == "Planet1")
            {
                print("Player1 died");
                gameManager.PlayerDied(1);
            }
            else if (gameObject.name == "Planet2")
            {
                print("Player2 died");
                gameManager.PlayerDied(2);
            }
            return;
        }

    }

    private void heal()
    {
        currentHealth = currentHealth + starPower;
        float healthRatio = currentHealth / startingHealth;
        transform.localScale = originalScale * healthRatio;

    }




}
