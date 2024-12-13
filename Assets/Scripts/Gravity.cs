using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityPuller2D : MonoBehaviour
{
    public float pullStrength = 1f; // Pull strength, adjustable in Inspector
    public float minDistance = 0.1f; // Minimum distance to prevent extreme forces

    private void FixedUpdate()
    {
        // Find all objects with this script
        GravityPuller2D[] objects = FindObjectsOfType<GravityPuller2D>();

        foreach (GravityPuller2D other in objects)
        {
            if (other == this) continue; // Skip self

            // Calculate the direction and distance
            Vector2 direction = other.transform.position - transform.position;
            float distance = direction.magnitude;

            // Skip if the distance is too small
            if (distance < minDistance) continue;

            // Normalize the direction
            direction.Normalize();

            // Get sizes from the scale (you can customize this calculation)
            float thisSize = transform.localScale.x; // Assuming size is proportional to scale
            float otherSize = other.transform.localScale.x;

            // Calculate the pull force magnitude
            float forceMagnitude = (thisSize/10 * otherSize/10 * pullStrength) / (distance * distance);
            Vector2 force = direction * forceMagnitude;

            // Apply the force to this object's Rigidbody2D
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(force);
            }
        }
    }
}





