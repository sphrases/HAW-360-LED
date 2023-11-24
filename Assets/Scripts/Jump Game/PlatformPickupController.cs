using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPickupController : MonoBehaviour
{
    public int PlatformAmount;
    public GameObject PlatformSymbolPrefab;
    public float PlatformSymbolDistance = 0.15f;

    void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        for(int i = 0; i < PlatformAmount; i++)
        {
            Vector3 _spawnPosition = new Vector3(transform.position.x, transform.position.y + i * PlatformSymbolDistance, transform.position.z);
            GameObject _spawnedPlatform = Instantiate(PlatformSymbolPrefab, _spawnPosition, transform.rotation);
            _spawnedPlatform.transform.parent = transform;
        }
    }

    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
