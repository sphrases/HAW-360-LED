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
        Vector2 _newPosition = ThisPlayersController.TransferCylinderPositionToFlatscreen();

        if (!UseXPosition)
        {
            _newPosition.x = transform.position.x;
        }
        if (!UseYPosition)
        {
            _newPosition.y = transform.position.y;
        }

        transform.localPosition = new Vector3(_newPosition.x, _newPosition.y, 0.01f);
    }

    void UpdateRotation()
    {
        if(!UseZRotation)
        {
            return;
        }

        float _targetZRotation = ThisPlayersController.GetPlayerZRotationEuler();

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

        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, _targetZRotation);
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

    public void SetPlayersController(CylinderToFlatscreenPosition newPlayerController) 
    {
        ThisPlayersController = newPlayerController;
    }

}
