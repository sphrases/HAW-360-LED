using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierSpawner : MonoBehaviour
{
    public GameObject ObstaclePrefab;
    public float SpawnInterval = 2f;

    void OnEnable()
    {
        StartCoroutine(SpawnCoroutine());
    }

    IEnumerator SpawnCoroutine()
    {
        float _elapsedTime = 0f;

        while(true)
        {
            _elapsedTime += Time.deltaTime; 

            if( _elapsedTime > SpawnInterval)
            {
                _elapsedTime = 0f;
                Instantiate(ObstaclePrefab, transform.position, transform.rotation);
            }

            yield return null;
        }
    }
}
