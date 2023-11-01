using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float MinXPosition = -960f;
    public float MaxXPosition = 960f;

    // Update is called once per frame
    void Update()
    {
        CheckXPosition();    
    }

    void CheckXPosition()
    {
        if(transform.position.x < MinXPosition)
        {
            transform.position = new Vector3(MaxXPosition, transform.position.y, transform.position.z);
        }
        else if(transform.position.x > MaxXPosition)
        {
            transform.position = new Vector3(MinXPosition, transform.position.y, transform.position.z);
        }
    }
}
