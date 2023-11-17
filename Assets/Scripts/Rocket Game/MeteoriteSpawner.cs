using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;

public class MeteoriteSpawner : MonoBehaviour
{
    public bool SpawnRandom = true;
    public SpawnInfo ThisSpawnInfo;
    public GameObject MeteoritePrefab;
    public int SpawnPositionAmount = 18;
    public float RandomSpawnInterval = 0.5f;

    RectTransform flatscreenRectTransform;

    private void Start()
    {
        flatscreenRectTransform = GetComponentInParent<RectTransform>();

        if (SpawnRandom)
        {
            StartCoroutine(RandomSpawnCoroutine());
        }
        else
        {
            StartCoroutine(SpawnCoroutine());
        }
    }

    IEnumerator SpawnCoroutine()
    {
        int _amountOfSpawnedElements = 0;
        float _elapsedTime = 0f;

        while (_amountOfSpawnedElements < ThisSpawnInfo.SpawnInfoElements.Count)
        {
            if (_elapsedTime >= ThisSpawnInfo.SpawnInfoElements[_amountOfSpawnedElements].SpawnTime)
            {
                SpawnMeteorite(_amountOfSpawnedElements);
                _amountOfSpawnedElements++;
            }

            _elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    void SpawnMeteorite(int _amountOfSpawnedElements)
    {
        GameObject _meteorite = Instantiate(MeteoritePrefab, transform.position, Random.rotation);
        Vector3 _initialPosition = MeteoritePrefab.transform.position;
        float _columnDistance = flatscreenRectTransform.rect.width / SpawnPositionAmount;

        float _newXPosition = _initialPosition.x + _columnDistance * ThisSpawnInfo.SpawnInfoElements[_amountOfSpawnedElements].SpawnPosition - (_columnDistance * SpawnPositionAmount / 2);
        float _newYPosition = _initialPosition.y + flatscreenRectTransform.rect.height / 2;
        float _newZPosition = _initialPosition.z + transform.root.position.z;

        Vector3 _newPosition = new Vector3(_newXPosition, _newYPosition, _newZPosition);
        _meteorite.transform.localPosition = _newPosition;
    }

    IEnumerator RandomSpawnCoroutine()
    {
        while (true)
        {
            SpawnMeteoriteRandomly();
            yield return new WaitForSeconds(RandomSpawnInterval);
        }
    }

    void SpawnMeteoriteRandomly()
    {
        GameObject _meteorite = Instantiate(MeteoritePrefab, transform.position, Random.rotation);
        Vector3 _initialPosition = MeteoritePrefab.transform.position;
        float _columnDistance = flatscreenRectTransform.rect.width / SpawnPositionAmount;
        int _spawnColumn = Random.Range(0, SpawnPositionAmount);

        float _newXPosition = _initialPosition.x + _columnDistance * _spawnColumn - (_columnDistance * SpawnPositionAmount / 2);
        float _newYPosition = _initialPosition.y + flatscreenRectTransform.rect.height / 2;
        float _newZPosition = _initialPosition.z + transform.root.position.z;

        Vector3 _newPosition = new Vector3(_newXPosition, _newYPosition, _newZPosition);
        _meteorite.transform.localPosition = _newPosition;
    }
}
