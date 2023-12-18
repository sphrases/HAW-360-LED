using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScroller : MonoBehaviour
{
    public float XPositionBounds = 9.6f;
    public float ScrollSpeed = -4f;

    private void Update()
    {
        transform.Translate(ScrollSpeed * Time.deltaTime, 0f, 0f);
        CheckXPosition();
    }

    void CheckXPosition()
    {
        if (transform.position.x < -XPositionBounds)
        {
            transform.position = new Vector3(XPositionBounds, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > XPositionBounds)
        {
            transform.position = new Vector3(-XPositionBounds, transform.position.y, transform.position.z);
        }
    }
}
