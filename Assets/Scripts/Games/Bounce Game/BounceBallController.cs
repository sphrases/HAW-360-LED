using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBallController : MonoBehaviour
{
    public float XPositionBounds = 9.6f;
    public float MaxVelocity = 1.0f;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckXPosition();
        LimitVelocity();
    }

    void CheckXPosition()
    {
        if (transform.position.x < -XPositionBounds)
        {
            transform.position = new Vector3(XPositionBounds, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > XPositionBounds)
        {
            transform.position = new Vector3(-XPositionBounds, transform.position.y, transform.position.z);
        }
    }

    void LimitVelocity()
    {
        if (rb.velocity.magnitude > MaxVelocity)
        {
            rb.velocity = Vector3.Lerp(rb.velocity, rb.velocity.normalized * MaxVelocity, 0.5f);
        }
    }
}
