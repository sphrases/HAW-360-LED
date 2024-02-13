using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RocketMovementController : MonoBehaviour
{
    public float Lifetime = 5f;
    public float Speed = 5f;
    public float XPositionBounds = 9.6f;

    private NavMeshAgent agent;
    private Rigidbody rb;
    private bool startedFollowing = false;
    private DamageDealer damageDealer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        damageDealer = GetComponent<DamageDealer>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Player"))
        {
            return;
        }

        if (!startedFollowing)
        {
            SetDestination(other.gameObject);
            startedFollowing = true;
        }
        else
        {
            damageDealer.DamageOtherPlayer(other);
        }
    }

    private void Update()
    {
        CheckXPosition();
    }

    public void SetDestination(GameObject _commandingPlayer)
    {
        agent = GetComponent<NavMeshAgent>();
        HealthController[] _healthControllers = FindObjectsOfType<HealthController>();

        for (int i = 0; i < _healthControllers.Length; i++)
        {
            if (_healthControllers[i].gameObject != _commandingPlayer)
            {
                GameObject _otherPlayer = _healthControllers[i].gameObject;
                StartCoroutine(FollowOtherPlayerCoroutine(_otherPlayer));
                return;
            }
        }

        StartMovingForward();
    }

    IEnumerator FollowOtherPlayerCoroutine(GameObject _otherPlayer)
    {
        while (true)
        {
            Vector3 _destination = _otherPlayer.transform.position;
            agent.SetDestination(_destination);
            yield return new WaitForSeconds(0.2f);
        }
    }

    private IEnumerator LifetimeCountdown()
    {
        yield return new WaitForSeconds(Lifetime);
        Destroy(gameObject);
    }

    private void StartMovingForward()
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
}
