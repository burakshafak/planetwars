using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour
{
    private Vector3 originalScale;
    private float currentHealth;
    [SerializeField] private float damage = 100f;
    public float startingHealth;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }


    void Start()
    {
        originalScale = transform.localScale;
        startingHealth = originalScale.x * 100;
        currentHealth = startingHealth;

    }

   
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fire1") || other.CompareTag("Fire2"))
        {
            print(gameObject.name + " is hit by a bullet.");
            currentHealth = currentHealth - damage;
            float healthRatio = currentHealth / startingHealth;
            transform.localScale = originalScale * healthRatio;
            audioManager.GameSFX(audioManager.damage);


            if (transform.localScale.x < 0.1)
            {
                Destroy(gameObject);
            }
        }             

    }
}
