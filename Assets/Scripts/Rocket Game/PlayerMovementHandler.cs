using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovementHandler : MonoBehaviour
{
    public float XPositionBounds = 9.6f;
    public float PlayerSpeed = 5f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        ClampZRotation();
        CheckXPosition();
        SetVelocity();
    }

    void SetVelocity()
    {
        Vector3 _localUp = transform.up * PlayerSpeed;
        Vector3 _worldUp = transform.TransformDirection(_localUp);
        rb.velocity = _localUp;
    }

    void ClampZRotation()
    {
        float _targetZRotation = transform.rotation.eulerAngles.z;

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

        if (_targetZRotation > 45f)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 45f);
        }
        else if (_targetZRotation < -45f)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, -45f);
        }
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
