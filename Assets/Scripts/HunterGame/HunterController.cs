using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HunterController : MonoBehaviour
{
    public float Speed = 5f;
    public float XPositionBounds = 9.6f;

    private NavMeshAgent agent;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        SetDestination();
    }

    private void Update()
    {
        CheckXPosition();
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

    public void SetDestination()
    {
        PlayerPhysicsBodyTransformHandler[] _playerTransformHandler = FindObjectsOfType<PlayerPhysicsBodyTransformHandler>();

        if(_playerTransformHandler != null)
        {
            GameObject _otherPlayer = _playerTransformHandler[0].gameObject;
            StartCoroutine(FollowOtherPlayerCoroutine(_otherPlayer));
        }
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
}
