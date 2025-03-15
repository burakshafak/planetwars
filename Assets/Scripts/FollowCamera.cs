using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    [SerializeField] GameObject MainCam;
    private float speed = 160f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, MainCam.transform.position, speed * Time.deltaTime);
    }
}
