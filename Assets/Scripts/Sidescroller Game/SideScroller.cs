using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScroller : MonoBehaviour
{
    public float ScrollSpeed = -4f;

    private void Update()
    {
        transform.Translate(ScrollSpeed * Time.deltaTime, 0f, 0f);
    }
}
