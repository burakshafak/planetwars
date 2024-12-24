using UnityEngine;

using System.Collections;
using System.Collections.Generic;


public class PlanetHP : MonoBehaviour
{
    private Vector3 originalScale;
    private float currentHealth;
    
    public float startingHealth = 100;

    public GameManager gameManager;

    [SerializeField] private  float starPower = 5f;
    [SerializeField] private float astreoidDamage = 1f;
    [SerializeField] private float fireDamage = 2f;

    private Vector3 damageRate;
    private Vector3 healRate;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

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
        if (gameObject.name == "Planet1")
        {
            if (other.CompareTag("Fire2"))
            {
                Destroy(other.gameObject); // Destroy the asteroid.
                TakeFireDamage(); // Example damage value.
                audioManager.GameSFX(audioManager.damage);

            }
        }

        if (gameObject.name == "Planet2")
        {
            if (other.CompareTag("Fire1"))
            {
                

                Destroy(other.gameObject); // Destroy the asteroid.
                TakeFireDamage(); // Example damage value.
                audioManager.GameSFX(audioManager.damage);

            }
        }

        if (other.CompareTag("Star"))
        {
            

            healRate = other.transform.localScale;
            print("Collected a star.");
            heal(healRate);
            audioManager.GameSFX(audioManager.star);

            Destroy(other.gameObject);
        }

        if (other.CompareTag("Astreoid"))
        {

            damageRate = other.transform.localScale;
            print("Collided with an astreoid.");
            TakeAstreoidDamage(damageRate);
            audioManager.GameSFX(audioManager.astreoid);

            Destroy(other.gameObject);  

        }


    }

    private void TakeFireDamage()
    {
        currentHealth -= fireDamage;
        audioManager.GameSFX(audioManager.damage);

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

    private void heal(Vector3 healRatee)
    {
        currentHealth = currentHealth + (starPower + healRatee.x);
        float healthRatio = currentHealth / startingHealth;
        transform.localScale = originalScale * healthRatio;
        audioManager.GameSFX(audioManager.star);
    }

}
