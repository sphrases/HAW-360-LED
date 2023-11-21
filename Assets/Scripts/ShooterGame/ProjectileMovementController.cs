using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovementController : MonoBehaviour
{
    public float Speed = 5f;
    public float XPositionBounds = 9.6f;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        MoveForward();
    }

    private void Update()
    {
        CheckXPosition();
    }

    private void MoveForward()
    {
        rb.velocity = transform.right * Speed * Time.deltaTime;
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
}
