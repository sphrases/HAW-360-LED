using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlatscreenPlayerTransformHandler : MonoBehaviour
{
    public GameObject ThisPlayersController;

    // Update is called once per frame
    void Update()
    {
        UpdateTransform();
    }

    void UpdateTransform()
    {
        Vector2 _newPosition = ThisPlayersController.GetComponent<CylinderToFlatscreenPosition>().GetPlayerPosition();
        transform.localPosition = new Vector3(_newPosition.x, _newPosition.y, 0.01f);


        Vector3 targetRotation = ThisPlayersController.GetComponent<CylinderToFlatscreenPosition>().GetPlayerZRotationEuler(); ;

        if(targetRotation.z > 180f)
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
}
