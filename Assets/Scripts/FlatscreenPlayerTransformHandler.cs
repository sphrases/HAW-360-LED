using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlatscreenPlayerTransformHandler : MonoBehaviour
{
    public CylinderToFlatscreenPosition ThisPlayersController;
    public bool UseXPosition = true;
    public bool UseYPosition = true;
    public bool UseZRotation = true;

    private bool updateTransform = true;


    private void Start()
    {
        if(!gameObject.CompareTag("Pointer"))
        {
            GameManager.Instance.GameMenuActivated += StopTransformUpdate;
            GameManager.Instance.GameMenuDeactivated += StartTransformUpdate;
        }

        SetPlayerControllersCorrespondingInGamePlayer();
    }

    public void SetPlayerControllersCorrespondingInGamePlayer()
    {
        if (ThisPlayersController != null)
        {
            ThisPlayersController.CorrespondingInGamePlayer = this;
        }
    }

    void Update()
    {
        UpdateTransform();
    }

    void UpdateTransform()
    {
        if(!updateTransform)
        {
            return;
        }

        if(ThisPlayersController == null)
        {
            return;
        }

        UpdatePosition();
        UpdateRotation();
    }

    void UpdatePosition()
    {
        Vector2 _newPosition = ThisPlayersController.GetPlayerPosition();

        if (!UseXPosition)
        {
            _newPosition.x = transform.localPosition.x;
        }
        if (!UseYPosition)
        {
            _newPosition.y = transform.localPosition.y;
        }

        transform.localPosition = new Vector3(_newPosition.x, _newPosition.y, 0.01f);
    }

    void UpdateRotation()
    {
        if(!UseZRotation)
        {
            return;
        }

        Vector3 targetRotation = ThisPlayersController.GetPlayerZRotationEuler(); ;

        if (targetRotation.z > 180f)
        {
            while (targetRotation.z > 180f)
            {
                targetRotation.z -= 180f;
            }

            targetRotation.z -= 180f;
        }
        else if (targetRotation.z < -180f)
        {
            while (targetRotation.z < -180f)
            {
                targetRotation.z += 180f;
            }

            targetRotation.z += 180f;
        }

        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, targetRotation.z);
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
        GameManager.Instance.GameMenuDeactivated -= StartTransformUpdate;
        GameManager.Instance.GameMenuActivated -= StopTransformUpdate;
    }
}
