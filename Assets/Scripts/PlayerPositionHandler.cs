using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionHandler : MonoBehaviour
{
    public GameObject ThisPlayersController;

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
    }

    void UpdatePosition()
    {
        Vector2 _newPosition = ThisPlayersController.GetComponent<CylinderToFlatscreenPosition>().GetPlayerPosition();
        transform.position = new Vector3(_newPosition.x, _newPosition.y, 0.01f);
    }
}
