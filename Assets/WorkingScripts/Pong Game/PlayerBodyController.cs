using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerBodyController : MonoBehaviour
{
    public GameObject Player;
    public float MaxDisplacement = 100f;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.position = Player.transform.position;
    }

    private void Update()
    {
        MoveToPlayerPosition();
        RotateToPlayerRotation();
    }

    void MoveToPlayerPosition()
    {
        Vector3 _displacement = Player.transform.position - transform.position;

        if (_displacement.magnitude > MaxDisplacement) // teleport 
        {
            transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, transform.position.z);
        }
        else // move to playerposition using physics
        {
            Vector3 _velocity = _displacement / Time.fixedDeltaTime;
            rb.velocity = _velocity;
        }
    }

    void RotateToPlayerRotation()
    {
        float _zAngleDifference = Player.transform.rotation.z - transform.rotation.z;
        float _angularVelocity = _zAngleDifference / Time.fixedDeltaTime;
        rb.angularVelocity = new Vector3(transform.rotation.x, transform.rotation.y, _angularVelocity);
    }
}
