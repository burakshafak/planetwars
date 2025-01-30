using UnityEngine;
using System;

public class Planet2Controller : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 150;

    public GameObject bulletPrefab;
    [SerializeField] public float bulletSpeed = 12;

    public GameObject planet2;

    [SerializeField] private float fireCooldown = 0.5f;

    private float lastFireTime = 0f;

    public GameObject shootingPoint;

    AudioManager audioManager;

    private Vector3 scale;

    Transform childTransform;

    // Velocity vector for inertia
    private Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
        {
            childTransform = transform.Find("Planet2");
            if (childTransform == null)
            {
                print("child transform could not be found");
            }

            scale = childTransform.localScale;
        }

        // Get player input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate input-based speed
        float horizontalSpeed = (horizontalInput * movementSpeed) / (float)Math.Sqrt(scale.x);
        float verticalSpeed = (verticalInput * movementSpeed) / (float)Math.Sqrt(scale.x);

        // Add inertia to movement
        if (scale.x > 0.01f)
        {
            // Target velocity from input
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

            // Handle firing
            if (Input.GetButtonUp("Fire2") && Time.time > lastFireTime + fireCooldown)
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
