using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovementController : MonoBehaviour
{
    public float XPositionBounds = 9.6f;
    public float MaxVelocity = 1.0f;
    public List<ParticleSystem> particles = new List<ParticleSystem>();

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckXPosition();
        LimitVelocity();
    }

    void CheckXPosition()
    {
        if(transform.position.x < -XPositionBounds)
        {
            PauseParticles();
            transform.position = new Vector3(XPositionBounds, transform.position.y, transform.position.z);
            Invoke("ContinueParticles", 0.01f);
        }
        else if(transform.position.x > XPositionBounds)
        {
            PauseParticles();
            transform.position = new Vector3(-XPositionBounds, transform.position.y, transform.position.z);
            Invoke("ContinueParticles", 0.01f);
        }
    }

    void LimitVelocity()
    {
        if(rb.velocity.magnitude > MaxVelocity)
        {
            rb.velocity = Vector3.Lerp(rb.velocity, rb.velocity.normalized * MaxVelocity, 0.5f);
        }
    }

    void PauseParticles()
    {
        foreach(ParticleSystem particle in particles)
        {
            particle.Stop();
            particle.Clear();
        }
    }

    void ContinueParticles()
    {
        foreach (ParticleSystem particle in particles)
        {
            particle.Play();
        }
    }
}
