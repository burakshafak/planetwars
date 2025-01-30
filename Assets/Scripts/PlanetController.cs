using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlanetController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 96;

    public GameObject bulletPrefab;
    [SerializeField] public float bulletSpeed = 10;

    public GameObject planet1;

    [SerializeField] private float fireCooldown = 1f;

    private float lastFireTime = 0f;

    public GameObject shootingPoint;

    AudioManager audioManager;

    private Vector3 scale;
    Transform childTransform;

    private Vector3 velocity = Vector3.zero;



    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    // Update is called once per frame
    void Update()
    {
        if(gameObject.activeSelf)
        {
            childTransform = transform.Find("Planet1");
            if (childTransform == null)
            {
                print("child transform could not be found");
            }

            scale = childTransform.localScale;
        }
        
        
        //get the Input from Horizontal axis
        float horizontalInput = Input.GetAxis("Player1 Horizontal");
        //get the Input from Vertical axis
        float verticalInput = Input.GetAxis("Player1 Vertical");

        float horizontalSpeed = (horizontalInput * movementSpeed) / (float)Math.Sqrt(scale.x);
        float verticalSpeed = (verticalInput * movementSpeed) / (float)Math.Sqrt(scale.x);

        if(scale.x > 0.01)
        {
            Vector3 targetVelocity = new Vector3(horizontalSpeed, verticalSpeed, 0);

            // Smoothly interpolate velocity toward the target
            velocity = Vector3.Lerp(velocity, targetVelocity, 0.01f);

            // Apply the velocity to move the planet
            transform.position += velocity * Time.deltaTime;

            // Optional: Gradual drag when no input is provided
            if (horizontalInput == 0 && verticalInput == 0)
            {
                velocity = Vector3.Lerp(velocity, Vector3.zero, 0.001f); // Adjust drag strength with 0.01f
            }

            if (Input.GetButtonUp("Fire1") && Time.time > lastFireTime + fireCooldown)
            {
                Fire();
                lastFireTime = Time.time;
            }
        }
        

    }

    
    void Fire()
    {
        audioManager.GameSFX(audioManager.fire);
        var bullet = Instantiate(bulletPrefab, shootingPoint.transform.position, shootingPoint.transform.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = shootingPoint.transform.up * bulletSpeed;
    }
}



