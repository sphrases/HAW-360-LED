using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRespawner : MonoBehaviour
{
    public float YRespawnThreshold = -6f;
    public Transform RespawnPosition;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        CheckYPosition();
    }

    void CheckYPosition()
    {
        if(transform.position.y < YRespawnThreshold)
        {
            transform.position = RespawnPosition.position;
            rb.velocity = Vector3.zero;   
        }
    }
}
