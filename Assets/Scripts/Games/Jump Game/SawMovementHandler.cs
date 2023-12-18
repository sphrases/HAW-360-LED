using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawMovementHandler : MonoBehaviour
{
    public List<Transform> Waypoints;
    public float MoveSpeed = 2f;

    bool updateTransform = true;
    int nextWaypoint = 0;

    private void Start()
    {
        GameManager.Instance.GameMenuActivated += StopTransformUpdate;
        GameManager.Instance.GameMenuDeactivated += StartTransformUpdate;
    }

    private void Update()
    {
        Move();
    }

    void Move()
    {
        if(Waypoints.Count == 0)
        {
            return;
        }

        if (!updateTransform)
        {
            return;
        }

        Vector3 _distance = (Waypoints[nextWaypoint].position - transform.position);

        if (_distance.magnitude < 0.05f)
        {
            nextWaypoint++;

            if(nextWaypoint == Waypoints.Count)
            {
                nextWaypoint = 0;
            }
        }

        Vector3 _direction = (Waypoints[nextWaypoint].position - transform.position).normalized;
        transform.Translate(_direction * MoveSpeed * Time.deltaTime);
    }

    void StopTransformUpdate()
    {
        updateTransform = false;
    }

    void StartTransformUpdate()
    {
        updateTransform = true;
    }
}
