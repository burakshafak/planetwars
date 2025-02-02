using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroyed : MonoBehaviour
{
    [SerializeField] private float bulletLifeTime = 10f;
    void Start()
    {
        //bullets destroyed after a certain amount of time
        Destroy(gameObject, bulletLifeTime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            print("Bullet hit " + collision.gameObject.name);

            if (gameObject.tag == "Fire1")
            {
                if (collision.gameObject.name == "Planet2")
                {
                    Destroy(gameObject);
                }
            }
            else if (gameObject.tag == "Fire2")
            {
                if (collision.gameObject.name == "Planet1")
                {
                    Destroy(gameObject);
                }
            }

            if (collision.CompareTag("Astreoid") || collision.CompareTag("Star"))
            {
                print("Bullet hit:" + collision.gameObject);
                Destroy(gameObject);
                
            }


        }
    }


}

