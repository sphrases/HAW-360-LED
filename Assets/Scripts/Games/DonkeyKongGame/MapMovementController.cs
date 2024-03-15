using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMovementController : MonoBehaviour
{
    public float MoveDownSpeed = 1.0f;

    private void Start()
    {
        StartCoroutine(MoveDown());
    }

    IEnumerator MoveDown()
    {
        yield return new WaitForSeconds(5f);

        while(true)
        {
            float newYValue = transform.position.y - MoveDownSpeed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, newYValue, transform.position.z);
            yield return null;
        }
    }
}
