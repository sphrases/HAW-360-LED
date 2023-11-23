using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteController : MonoBehaviour
{
    public float YMoveSpeed = -200f;
    public float LifeTime = 20f;

    bool updateTransform = true;

    private void Start()
    {
        GameManager.Instance.GameMenuActivated += StopTransformUpdate;
        GameManager.Instance.GameMenuDeactivated += StartTransformUpdate;
    }

    private void OnEnable()
    {
        StartCoroutine(LifetimeCountdown());
    }

    private void OnDisable()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        if (!updateTransform)
        {
            return;
        }

        float newYCoordinate = transform.position.y + YMoveSpeed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, newYCoordinate, transform.position.z);
    }

    IEnumerator LifetimeCountdown()
    {
        float _elapsedTime = 0f;

        while(_elapsedTime < LifeTime)
        {
            if(updateTransform)
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
