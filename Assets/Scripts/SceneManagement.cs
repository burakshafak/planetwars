using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    void Update()
    {
        // Check if the Space key is pressed
        if (Input.anyKey)
        {
            // Load the SampleScene
            SceneManager.LoadScene("SampleScene");
        }
    }
}
