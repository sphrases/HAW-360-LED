using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlayerController : MonoBehaviour
{
    public float JumpForce = 50f;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Spikes"))
        {
            Destroy(gameObject);
        }
        else if (collider.gameObject.CompareTag("Platform"))
        {
            Jump(collider);
        }
    }

    void Jump(Collider collider)
    {
        if (collider.transform.position.y > transform.position.y)
        {
            return;
        }

        rb.velocity = new Vector3(rb.velocity.x, JumpForce, rb.velocity.z);
    }
}
