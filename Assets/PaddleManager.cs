using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PaddleManager : MonoBehaviour
{
    // Public
    public Transform paddleOrigin;

    public Transform targetCylinder;

    public GameObject pointerPrefab;

    public Material activeMaterial;

    public float maxRange;


    // Private
    private GameObject _pointerReference;

    private Material _defaultMaterialReference;

    private float _distance;

    void Start()
    {
        _pointerReference = Instantiate(pointerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        _defaultMaterialReference = _pointerReference.GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        var paddleOriginPos = paddleOrigin.position;
        var heading = targetCylinder.position - paddleOriginPos;
        _distance = heading.magnitude;
        var direction = heading / _distance; // This is now the normalized direction.

        
        _pointerReference.transform.position = paddleOriginPos;
        _pointerReference.transform.rotation = Quaternion.LookRotation(direction);
        Debug.DrawRay(transform.position, direction, Color.green);


        SetMaterial(heading.sqrMagnitude < maxRange * maxRange); // should be done via events, i know...
    }

    void SetMaterial(Boolean active)
    {
        if (active)
        {

            var newColor = activeMaterial.color;
            newColor.a = _distance;

            activeMaterial.SetColor("_green", newColor);
            _pointerReference.GetComponent<MeshRenderer>().material = activeMaterial;

        }
        else
        {
            _pointerReference.GetComponent<MeshRenderer>().material = _defaultMaterialReference;
        }
    }
}