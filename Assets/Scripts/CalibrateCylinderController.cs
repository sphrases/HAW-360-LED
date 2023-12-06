using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CalibrateCylinderController : MonoBehaviour
{

    private bool _calibratingCylinderPosition = false;
    public GameObject ControllerManager3D;


    private float _cylinderCenterOffset;



    public void Start()
    {
        _cylinderCenterOffset = transform.localScale.x / 2;
    }

    // Update is called once per frame
    void Update()
    {


        if (_calibratingCylinderPosition)
        {
            updateCalibrateCylinder();
        }


    }


    private void updateCalibrateCylinder()
    {

        Transform newTransform = ControllerManager3D.transform;


        newTransform.rotation = Quaternion.Euler(0, newTransform.rotation.eulerAngles.y, 0);

        Vector3 newWorldPos = newTransform.TransformPoint(new Vector3(0,0,_cylinderCenterOffset));

        newWorldPos = new Vector3(newWorldPos.x, transform.position.y, newWorldPos.z);

        transform.position = newWorldPos;


    }


    public void setCalibratingCylinder(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _calibratingCylinderPosition = !_calibratingCylinderPosition;
        }

    }


}
