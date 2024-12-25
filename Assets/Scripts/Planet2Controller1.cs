using UnityEngine;
using System;
public class Planet2Controller : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 80;

    public GameObject bulletPrefab;
    [SerializeField] public float bulletSpeed = 6;

    public GameObject planet2;

    [SerializeField] private float fireCooldown = 2f;

    private float lastFireTime = 0f;

    public GameObject shootingPoint;

    AudioManager audioManager;

    private Vector3 scale;

    Transform childTransform;

   

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

        float horizontalInput = Input.GetAxis("Horizontal");
        //get the Input from Vertical axis
        float verticalInput = Input.GetAxis("Vertical");

        float horizontalSpeed = (horizontalInput * movementSpeed) /(float)Math.Sqrt(scale.x);
        float verticalSpeed = (verticalInput * movementSpeed) /(float)Math.Sqrt (scale.x);

        transform.position = transform.position + new Vector3(horizontalSpeed * Time.deltaTime, verticalSpeed * Time.deltaTime, 0);

        if (Input.GetButtonUp("Fire2") && Time.time > lastFireTime + fireCooldown)
        {
            Fire();
            lastFireTime = Time.time;
        }


    }

    void Fire()
    {
        audioManager.GameSFX(audioManager.fire);
        var bullet = Instantiate(bulletPrefab, shootingPoint.transform.position, shootingPoint.transform.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = shootingPoint.transform.up * bulletSpeed;



    }
}

