using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongBallJuice : MonoBehaviour
{
    private Rigidbody rb; // Reference to the Rigidbody component of the sphere
    public float maxScale = 2.0f; // Maximum scale factor
    public float minScale = 0.5f; // Minimum scale factor
    public float scaleSpeed = 0.1f; // Speed of scaling

    private Vector3 previousVelocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        previousVelocity = rb.velocity;
    }

    private void Update()
    {
        // Get the change in velocity since the last frame
        Vector3 velocityChange = rb.velocity - previousVelocity;

        // Calculate the scale factor based on the change in velocity
        float scaleFactor = Mathf.Clamp(velocityChange.magnitude * scaleSpeed, minScale, maxScale);

        // Calculate the direction of the change in velocity
        Vector3 direction = velocityChange.normalized;

        // Calculate the new scale vector based on the direction of velocity change
        Vector3 newScale = Vector3.Lerp(transform.localScale, Vector3.one + direction * scaleFactor, Time.deltaTime);

        // Apply the new scale
        transform.localScale = newScale;

        // Update the previous velocity for the next frame
        previousVelocity = rb.velocity;
    }
}
