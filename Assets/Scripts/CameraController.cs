using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject planet1;
    [SerializeField] GameObject planet2;

    private float cameraSpeed = 170.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position1 = planet1.transform.position;
        Vector3 position2 = planet2.transform.position;

        Vector3 middlePoint = (position1 + position2)/2;

        Vector3 movePoint = new(middlePoint.x, middlePoint.y, transform.position.z);

        
            transform.position = Vector3.MoveTowards(transform.position, movePoint, cameraSpeed * Time.deltaTime);

        

    }
}
