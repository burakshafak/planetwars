using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour
{
    private Vector3 originalScale;
    private float currentHealth;
    [SerializeField] private float damage = 20f;
    public float startingHealth;
    // Start is called before the first frame update
    void Start()
    {
        originalScale = transform.localScale;
        startingHealth = originalScale.x * 100;
        currentHealth = startingHealth;
        print("The name of the game object: " + gameObject.name);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fire1") || other.CompareTag("Fire2"))
        {
            currentHealth = currentHealth - damage;
            float healthRatio = currentHealth / startingHealth;
            transform.localScale = originalScale * healthRatio;

            if (transform.localScale.x < 0.1)
            {
                Destroy(gameObject);
            }
        }
        
        

    }
}
