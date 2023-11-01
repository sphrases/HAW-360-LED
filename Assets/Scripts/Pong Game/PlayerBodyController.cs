using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerBodyController : MonoBehaviour
{
    public GameObject Player;
    public float MaxDisplacement = 100f;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.position = Player.transform.position;
    }

    private void Update()
    {
        Vector3 _displacement = Player.transform.position - transform.position;

        if (_displacement.magnitude > MaxDisplacement)
        {
            transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, transform.position.z);
        }
        else
        {
            Vector3 _velocity = _displacement / Time.fixedDeltaTime;
            rb.velocity = _velocity;
        }
    }
}
