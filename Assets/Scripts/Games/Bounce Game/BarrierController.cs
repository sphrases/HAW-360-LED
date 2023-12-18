using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierController : MonoBehaviour
{
    public float YMoveSpeed = -1f;

    private void Start()
    {
        Destroy(gameObject, 10f);
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.Translate(0f, YMoveSpeed * Time.deltaTime, 0f);
    }
}
