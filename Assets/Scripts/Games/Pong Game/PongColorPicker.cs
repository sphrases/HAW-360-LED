using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongColorPicker : MonoBehaviour
{
    public Material LeftControllerMaterial;
    public Material RightControllerMaterial;


    private void Start()
    {
        if(transform.parent.GetComponentInChildren<FlatscreenPlayerTransformHandler>().ThisPlayersController.CompareTag("CustomGameControllerLeft"))
        {
            GetComponent<MeshRenderer>().material = LeftControllerMaterial;
        }
        else
        {
            GetComponent<MeshRenderer>().material = RightControllerMaterial;
        }
    }
}
