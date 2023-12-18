using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ControllerManager3D : MonoBehaviour
{
    // Public
    public Transform xRControllerOrigin;

    public Transform targetCylinder;


    // Update is called once per frame
    void Update()
    {
        var paddleOriginPos = xRControllerOrigin.position;

        // move _self to paddleOrigin
        transform.position = paddleOriginPos;
        transform.rotation = xRControllerOrigin.rotation;
    }
}