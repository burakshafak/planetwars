using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroyed : MonoBehaviour
{
    [SerializeField]  private float bulletLifeTime = 10f;
    void Start()
    {
        //bullets destroyed after a certain amount of time
        Destroy(gameObject, bulletLifeTime);
    }

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            print("Collided with " + collision.gameObject.name);
            Destroy(gameObject);
        }
    }
   

}
