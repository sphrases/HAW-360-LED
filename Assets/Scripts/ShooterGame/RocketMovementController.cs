using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RocketMovementController : MonoBehaviour
{
    public float Lifetime = 5f;
    public float Speed = 5f;
    public float XPositionBounds = 9.6f;

    private Vector3 destination;
    private NavMeshAgent agent;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        StartCoroutine(LifetimeCountdown());
    }

    private void Update()
    {
        CheckXPosition();
    }

    public void SetDestination(GameObject _commandingPlayer)
    {
        agent = GetComponent<NavMeshAgent>();
        HealthController[] _healthControllers = FindObjectsOfType<HealthController>();
        bool _foundOtherPlayer = false;

        for (int i = 0; i < _healthControllers.Length; i++)
        {
            if (_healthControllers[i].gameObject != _commandingPlayer)
            {
                destination = _healthControllers[i].transform.position;
                _foundOtherPlayer = true;
                break;
            }
        }

        if(!_foundOtherPlayer)
        {
            MoveForward();
            return;
        }

        agent.SetDestination(destination);
    }

    private IEnumerator LifetimeCountdown()
    {
        yield return new WaitForSeconds(Lifetime);
        Destroy(gameObject);
    }

    private void MoveForward()
    {
        rb.velocity = transform.right * Speed * Time.deltaTime;
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

    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
