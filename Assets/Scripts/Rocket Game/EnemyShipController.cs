using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipController : MonoBehaviour
{
    public float YVelocity = 2f;
    public float XVelocity = 2f;
    public float LifeTime = 10f;

    DistanceCounter[] _distanceCounters;
    bool updateTransform = true;

    private void Start()
    {
        GameManager.Instance.GameMenuActivated += StopTransformUpdate;
        GameManager.Instance.GameMenuDeactivated += StartTransformUpdate;
        _distanceCounters = FindObjectsOfType<DistanceCounter>();
        MoveToClosestPlayer();
    }

    private void OnEnable()
    {
        StartCoroutine(LifetimeCountdown());
    }

    private void OnDisable()
    {
        Destroy(gameObject);
    }

    void MoveToClosestPlayer()
    {
        GameObject _closestPlayer = GetClosestPlayer();

        if (_closestPlayer == null)
        {
            return;
        }

        StartCoroutine(FollowPlayerCoroutine(_closestPlayer));
    }

    GameObject GetClosestPlayer()
    {
        if(_distanceCounters.Length == 0)
        {
            return null;
        }

        float _smallestXDistance = 100f;
        GameObject _closestPlayer = null;

        for (int i = 0; i < _distanceCounters.Length; i++)
        {
            float _xDistanceToPlayer = (transform.position.x - _distanceCounters[i].transform.position.x);

            if (_smallestXDistance > _xDistanceToPlayer)
            {
                _closestPlayer = _distanceCounters[i].gameObject;
                _smallestXDistance = transform.position.x - _distanceCounters[i].transform.position.x;
            }
        }

        return _closestPlayer;
    }

    IEnumerator FollowPlayerCoroutine(GameObject _closestPlayer)
    {
        while(true)
        {
            if(!updateTransform)
            {
                yield return null;
            }

            if(_closestPlayer.transform.position.x > transform.position.x)
            {
                float _xTranslation = XVelocity * Time.deltaTime;
                transform.Translate(Vector3.right * _xTranslation);
            }
            else
            {
                float _xTranslation = -XVelocity * Time.deltaTime;
                transform.Translate(Vector3.right * _xTranslation);
            }

            float _yTranslation = YVelocity * Time.deltaTime;
            transform.Translate(-Vector3.up * _yTranslation);
            yield return null;
        }
    }

    IEnumerator LifetimeCountdown()
    {
        float _elapsedTime = 0f;

        while (_elapsedTime < LifeTime)
        {
            if (updateTransform)
            {
                _elapsedTime += Time.deltaTime;
            }

            yield return null;
        }

        Destroy(gameObject);
    }

    void StopTransformUpdate()
    {
        updateTransform = false;
    }

    void StartTransformUpdate()
    {
        updateTransform = true;
    }

    private void OnDestroy()
    {
        GameManager.Instance.GameMenuActivated -= StopTransformUpdate;
        GameManager.Instance.GameMenuDeactivated -= StartTransformUpdate;
    }

    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
