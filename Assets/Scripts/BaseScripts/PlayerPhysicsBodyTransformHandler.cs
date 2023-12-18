using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerPhysicsBodyTransformHandler : MonoBehaviour
{
    public GameObject PlayerInGamePosition;
    public float MaxPositionDisplacement = 100f;
    public float MaxZAngleDifference = 100f;
    public float MaxVelocity = 0f;
    public bool UseXPosition = true;
    public bool UseYPosition = true;
    public bool UseZRotation = true;

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
        if (!UseXPosition && !UseYPosition)
        {
            return;
        }

        Vector3 _displacement = GetDisplacement();

        if (_displacement.magnitude > MaxPositionDisplacement) // teleport 
        {
            if(UseYPosition && UseXPosition)
            {
                transform.position = new Vector3(PlayerInGamePosition.transform.position.x, PlayerInGamePosition.transform.position.y, transform.position.z);
            }
            else if(!UseYPosition)
            {
                transform.position = new Vector3(PlayerInGamePosition.transform.position.x, transform.position.y, transform.position.z);
            }
            else if(! UseXPosition) 
            {
                transform.position = new Vector3(transform.position.x, PlayerInGamePosition.transform.position.y, transform.position.z);
            }
        }
        else // move to playerposition using physics
        {
            ApplyVelocity(_displacement);
        }
    }

    void ApplyVelocity(Vector3 _displacement)
    {
        Vector3 _velocity = _displacement / Time.fixedDeltaTime;

        if(UseXPosition && UseYPosition)
        {
            if (MaxVelocity > 0f)
            {
                rb.velocity = Vector3.ClampMagnitude(_velocity, MaxVelocity);
            }
            else
            {
                rb.velocity = _velocity;
            }
        }
        else if(UseXPosition)
        {
            float _xVelocity = _velocity.x;

            if (MaxVelocity > 0f)
            {
                float _clampedXVelocity = 0f;

                if (_xVelocity < 0f)
                {
                    _clampedXVelocity = Mathf.Clamp(_xVelocity, -MaxVelocity, 0f);
                }
                else
                {
                    _clampedXVelocity = Mathf.Clamp(_xVelocity, 0f, MaxVelocity);
                }

                rb.velocity = new Vector3(_clampedXVelocity, rb.velocity.y, rb.velocity.z);
            }
            else
            {
                rb.velocity = new Vector3(_xVelocity, rb.velocity.y, rb.velocity.z);
            }
        }
        else
        {
            float _yVelocity = _velocity.y;

            if (MaxVelocity > 0f)
            {
                float _clampedYVelocity = 0f;

                if (_yVelocity < 0f)
                {
                    _clampedYVelocity = Mathf.Clamp(_yVelocity, -MaxVelocity, 0f);
                }
                else
                {
                    _clampedYVelocity = Mathf.Clamp(_yVelocity, 0f, MaxVelocity);
                }

                rb.velocity = new Vector3(_clampedYVelocity, rb.velocity.y, rb.velocity.z);
            }
            else
            {
                rb.velocity = new Vector3(rb.velocity.x, _yVelocity, rb.velocity.z);
            }
        }
    }

    Vector3 GetDisplacement()
    {
        if(UseXPosition && UseYPosition)
        {
            return PlayerInGamePosition.transform.position - transform.position;
        }
        else if(UseXPosition)
        {
            return new Vector3(PlayerInGamePosition.transform.position.x, transform.position.y, transform.position.z) - transform.position;
        }
        else
        {
            return new Vector3(transform.position.x, PlayerInGamePosition.transform.position.y, transform.position.z) - transform.position;
        }
    }

    void RotateToPlayerRotation()
    {
        if(!UseZRotation)
        {
            return;
        }

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