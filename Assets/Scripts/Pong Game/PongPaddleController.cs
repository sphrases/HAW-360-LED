using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PongPaddleController : MonoBehaviour
{
    public GameObject PlayerInGamePosition;
    public float MaxPositionDisplacement = 100f;
    public float MaxZAngleDifference = 100f;

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
        float _zAngleDifference = PlayerInGamePosition.transform.eulerAngles.z - transform.eulerAngles.z;

        if (Mathf.Abs(_zAngleDifference) > MaxZAngleDifference)
        {
            float _targetZRotation = PlayerInGamePosition.transform.eulerAngles.z;

            if (_targetZRotation > 180f)
            {
                while (_targetZRotation > 180f)
                {
                    _targetZRotation -= 180f;
                }

                _targetZRotation -= 180f;
            }
            else if (_targetZRotation < -180f)
            {
                while (_targetZRotation < -180f)
                {
                    _targetZRotation += 180f;
                }

                _targetZRotation += 180f;
            }

            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, _targetZRotation);
        }
        else
        {
            float _zAngleDifferenceRadians = _zAngleDifference * Mathf.Deg2Rad;
            float _zAngularVelocity = _zAngleDifferenceRadians / Time.fixedDeltaTime;
            rb.angularVelocity = new Vector3(transform.rotation.x, transform.rotation.y, _zAngularVelocity);
        }
    }
}