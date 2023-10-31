using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteController : MonoBehaviour
{
    public float YMoveSpeed = -200f;

    private void Update()
    {
        float newYCoordinate = transform.position.y + YMoveSpeed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, newYCoordinate, transform.position.z);
    }
}
