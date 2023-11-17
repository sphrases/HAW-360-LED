using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerBodyController : MonoBehaviour
{
    public GameObject PlayerInGamePosition;
    public float MaxPositionDisplacement = 100f;
    public float MaxRotationDisplacement = 90f;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.position = PlayerInGamePosition.transform.position;
    }

    private void Update()
    {
        MoveToPlayerPosition();
        RotateToPlayerRotation();
    }

    void MoveToPlayerPosition()
    {
        Vector3 _displacement = PlayerInGamePosition.transform.position - transform.position;

        if (_displacement.magnitude > MaxPositionDisplacement) // teleport 
        {
            transform.position = new Vector3(PlayerInGamePosition.transform.position.x, PlayerInGamePosition.transform.position.y, transform.position.z);
        }
        else // move to playerposition using physics
        {
            Vector3 _velocity = _displacement / Time.fixedDeltaTime;
            rb.velocity = _velocity;
        }
    }

    void RotateToPlayerRotation()
    {
        // if the displacement is too large the rotation should be changed instantly using the transform just like in MovePlayerPosition but it doesnt seem to work right

        float _zAngleDifference = PlayerInGamePosition.transform.rotation.z - transform.rotation.z;
        float _angularVelocity = _zAngleDifference / Time.fixedDeltaTime;
        rb.angularVelocity = new Vector3(transform.rotation.x, transform.rotation.y, _angularVelocity);
    }
}
