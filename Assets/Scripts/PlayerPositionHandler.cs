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
        RectTransform _rectTransform = GetComponent<RectTransform>();
        _rectTransform.anchoredPosition = new Vector3(_newPosition.x, _newPosition.y, 0);
    }
}
