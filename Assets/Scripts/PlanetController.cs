using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1;

    public GameObject bulletPrefab;
    [SerializeField] public float bulletSpeed = 1;

    public GameObject planet1;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //get the Input from Horizontal axis
        float horizontalInput = Input.GetAxis("Player1 Horizontal");
        //get the Input from Vertical axis
        float verticalInput = Input.GetAxis("Player1 Vertical");

        //update the position
        transform.position = transform.position + new Vector3(horizontalInput * movementSpeed * Time.deltaTime, verticalInput * movementSpeed * Time.deltaTime, 0);

        float fireInput = Input.GetAxis("Fire1");
        if(fireInput != 0)
        {
            Fire();
        }
        
    }

    void Fire()
    {
        print("Inside fire method.");
        var bullet = Instantiate(bulletPrefab, planet1.transform.position, planet1.transform.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = planet1.transform.up * bulletSpeed;
    }
}
