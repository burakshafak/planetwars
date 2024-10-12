using UnityEngine;

public class Planet2Controller : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1;

    public GameObject bulletPrefab;
    [SerializeField] public float bulletSpeed = 1;

    public GameObject planet2;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //get the Input from Horizontal axis
        float horizontalInput = Input.GetAxis("Horizontal");
        //get the Input from Vertical axis
        float verticalInput = Input.GetAxis("Vertical");

        //update the position
        transform.position = transform.position + new Vector3(horizontalInput * movementSpeed * Time.deltaTime, verticalInput * movementSpeed * Time.deltaTime, 0);

        float fireInput = Input.GetAxis("Fire2");
        if (fireInput != 0)
        {
            Fire();
        }

        
    }

    void Fire()
    {
        print("Inside fire method.");
        var bullet = Instantiate(bulletPrefab, planet2.transform.position, planet2.transform.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = planet2.transform.up * bulletSpeed;
    }
}
