using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject[] prefabs;     // Array of prefabs to spawn
    public Transform[] respawnPoints; // Array of predefined respawn points
    public float spawnInterval = 0.5f; // Time between spawns

    void Start()
    {
        // Start spawning at regular intervals
        InvokeRepeating(nameof(SpawnAtRandomRespawnPoint), 0f, spawnInterval);
    }

    void SpawnAtRandomRespawnPoint()
    {
        if (respawnPoints.Length == 0 || prefabs.Length == 0)
        {
            Debug.LogWarning("No respawn points or prefabs assigned!");
            return;
        }

        // Select a random respawn point
        Transform randomRespawn = respawnPoints[Random.Range(0, respawnPoints.Length)];

        // Select a random prefab to spawn
        GameObject randomPrefab = prefabs[Random.Range(0, prefabs.Length)];

        // Instantiate the prefab at the respawn point
        GameObject spawnedObject = Instantiate(randomPrefab, randomRespawn.position, randomRespawn.rotation);
        Vector3 fixedPosition = spawnedObject.transform.position;
        fixedPosition.z = 0f;
        spawnedObject.transform.position = fixedPosition;
        //GameWorld layer
        SetLayerRecursively(randomPrefab, 3);
    }

    void SetLayerRecursively(GameObject obj, int layer)
    {
        obj.layer = layer; // Set the object's layer

        // Apply the layer to all child objects as well
        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, layer);
        }
    }
}



