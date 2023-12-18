using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollHandler : MonoBehaviour
{
    public Transform JumpPlayerTransform;
    public Transform MaxPlayerYPosition;
    public GameObject Map;

    void Update()
    {
        CheckYPosition();
    }

    void CheckYPosition()
    {
        if(JumpPlayerTransform.position.y <= MaxPlayerYPosition.position.y)
        {
            return;
        }

        float _yDifference = JumpPlayerTransform.position.y - MaxPlayerYPosition.position.y;
        Map.transform.Translate(new Vector3(0f, -_yDifference, 0f));
    }
}
