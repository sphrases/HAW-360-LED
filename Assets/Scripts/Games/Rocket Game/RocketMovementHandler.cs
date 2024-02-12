using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RocketMovementHandler : MonoBehaviour
{
    public float XPositionBounds = 9.6f;
    public float PlayerSpeed = 5f;

    public float SineAmplitude = 1f; 
    public float SineFrequency = 1f; 
    private float sineOffset;

    private Rigidbody rb;

    void Start()
    {
        sineOffset = Random.Range(0f, 2f * Mathf.PI);
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        ClampZRotation();
        CheckXPosition();
        SetYPosition();
        SetVelocity();
    }

    void SetVelocity()
    {
        Vector3 _localUp = transform.up * PlayerSpeed;
        Vector3 _worldUp = transform.TransformDirection(_localUp);
        rb.velocity = _localUp;
    }

    void SetYPosition()
    {
        float newY = Mathf.Sin(Time.time * SineFrequency + sineOffset) * SineAmplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
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
