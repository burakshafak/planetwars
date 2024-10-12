using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1;
    

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

        
    }

   



    void Fire()
    {
       


    }
}
