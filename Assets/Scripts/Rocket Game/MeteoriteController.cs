using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteController : MonoBehaviour
{
    public float YMoveSpeed = -200f;

    bool updateTransform = true;

    private void Start()
    {
        GameManager.Instance.GameMenuActivated += StopTransformUpdate;
        GameManager.Instance.GameMenuDeactivated += StartTransformUpdate;
    }

    private void Update()
    {
        if (!updateTransform)
        {
            return;
        }

        float newYCoordinate = transform.position.y + YMoveSpeed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, newYCoordinate, transform.position.z);
    }

    void StopTransformUpdate()
    {
        updateTransform = false;
    }

    void StartTransformUpdate()
    {
        updateTransform = true;
    }

    private void OnDestroy()
    {
        GameManager.Instance.GameMenuActivated -= StopTransformUpdate;
        GameManager.Instance.GameMenuDeactivated -= StartTransformUpdate;
    }
}
