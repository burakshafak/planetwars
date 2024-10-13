using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1;

    public GameObject bulletPrefab;
    [SerializeField] public float bulletSpeed = 1;

    public GameObject planet1;

    [SerializeField] private float fireCooldown = 2f;

    private float lastFireTime = 0f;

    public GameObject shootingPoint;

  
    // Update is called once per frame
    void Update()
    {
        //get the Input from Horizontal axis
        float horizontalInput = Input.GetAxis("Player1 Horizontal");
        //get the Input from Vertical axis
        float verticalInput = Input.GetAxis("Player1 Vertical");

        //update the position
        transform.position = transform.position + new Vector3(horizontalInput * movementSpeed * Time.deltaTime, verticalInput * movementSpeed * Time.deltaTime, 0);

        if (Input.GetButtonUp("Fire1") && Time.time > lastFireTime + fireCooldown)
        {
            Fire();
            lastFireTime = Time.time;
        }

    }

    IEnumerator Firee()
    {
        var bullet = Instantiate(bulletPrefab, shootingPoint.transform.position, shootingPoint.transform.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = shootingPoint.transform.up * bulletSpeed;
        yield return null;
    }

    void Fire()
    {
        
        var bullet = Instantiate(bulletPrefab, shootingPoint.transform.position, shootingPoint. transform.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity =  shootingPoint.transform.up * bulletSpeed;
    }
}
