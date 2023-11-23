using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawRotationHandler : MonoBehaviour
{
    public float SpinSpeed = 2f;

    bool updateTransform = true;

    private void Start()
    {
        GameManager.Instance.GameMenuActivated += StopTransformUpdate;
        GameManager.Instance.GameMenuDeactivated += StartTransformUpdate;
    }
    void Update()
    {
        Spin();
    }

    void Spin()
    {
        if (!updateTransform)
        {
            return;
        }

        transform.Rotate(0f, 0f, SpinSpeed * Time.deltaTime, Space.Self);
    }

    void StopTransformUpdate()
    {
        updateTransform = false;
    }

    void StartTransformUpdate()
    {
        updateTransform = true;
    }
}
