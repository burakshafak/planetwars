using UnityEngine;

public class Planet2Controller : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1;

    public GameObject bulletPrefab;
    [SerializeField] public float bulletSpeed = 6;

    public GameObject planet2;

    [SerializeField] private float fireCooldown = 2f;

    private float lastFireTime = 0f;

    public GameObject shootingPoint;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //get the Input from Horizontal axis
        float horizontalInput = Input.GetAxis("Horizontal");
        //get the Input from Vertical axis
        float verticalInput = Input.GetAxis("Vertical");

        transform.position = transform.position + new Vector3(horizontalInput * movementSpeed * Time.deltaTime, verticalInput * movementSpeed * Time.deltaTime, 0);

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

