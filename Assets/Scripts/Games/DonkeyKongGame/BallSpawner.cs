using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject BallPrefab;
    public float SpawnInterval = 4f;

    void OnEnable()
    {
        StartCoroutine(BallSpawnCoroutine());
    }

    IEnumerator BallSpawnCoroutine()
    {
        float _elapsedTime = 3f;

        while (true)
        {
            _elapsedTime += Time.deltaTime;

            if(_elapsedTime > SpawnInterval)
            {
                Instantiate(BallPrefab, transform.position, transform.rotation);
                _elapsedTime = 0f;
            }

            yield return null;
        }
    }
}
