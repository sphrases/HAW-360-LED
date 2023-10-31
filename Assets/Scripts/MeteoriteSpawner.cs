using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;

public class MeteoriteSpawner : MonoBehaviour
{
    public SpawnInfo ThisSpawnInfo;
    public GameObject MeteoritePrefab;
    public float ColumDistance = 125f;

    private void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    IEnumerator SpawnCoroutine()
    {
        int _amountOfSpawnedElements = 0;
        float _elapsedTime = 0f;

        while(_amountOfSpawnedElements < ThisSpawnInfo.SpawnInfoElements.Count)
        {
            if (_elapsedTime >= ThisSpawnInfo.SpawnInfoElements[_amountOfSpawnedElements].SpawnTime)
            {
                GameObject _meteorite = Instantiate(MeteoritePrefab);
                Vector3 _initialPosition = MeteoritePrefab.transform.position;
                Vector3 _newPosition = new Vector3(_initialPosition.x + ColumDistance * ThisSpawnInfo.SpawnInfoElements[_amountOfSpawnedElements].SpawnPosition, _initialPosition.y, _initialPosition.z);
                _meteorite.transform.position = _newPosition;
                _amountOfSpawnedElements++;
            }
               
            _elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
