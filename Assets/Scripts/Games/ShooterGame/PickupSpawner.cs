using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PickupSpawner : MonoBehaviour
{
    public GameObject PickupPrefab;
    public int MaxSpawnedPickups = 2;
    public float XPositionBounds = 9.6f;
    public float YPositionBounds = 4.8f;

    int amountOfSpawnedElements = 0;
    RectTransform flatscreenRectTransform;

    public static PickupSpawner Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        flatscreenRectTransform = GetComponentInParent<RectTransform>();
    }

    private void Start()
    {
        GameManager.Instance.GameMenuActivated += StopSpawning;
        GameManager.Instance.GameMenuDeactivated += StartSpawning;
    }

    void OnEnable()
    {
        StartSpawning();
    }

    void StartSpawning()
    {
        StartCoroutine(SpawnCoroutine());
    }

    IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            if (amountOfSpawnedElements < MaxSpawnedPickups)
            {
                SpawnPickup();
                amountOfSpawnedElements++;
            }

            yield return null;
        }
    }

    public void ReduceSpawnedElements()
    {
        amountOfSpawnedElements--;
    }

    void SpawnPickup()
    {
        Vector3 _randomPositionOnNavMesh = GetRandomPositionOnNavMesh();
        Instantiate(PickupPrefab, _randomPositionOnNavMesh, Quaternion.identity, transform);
    }

    Vector3 GetRandomPositionOnNavMesh()
    {
        NavMeshHit hit;
        Vector3 randomPosition = new Vector3(UnityEngine.Random.Range(-XPositionBounds, XPositionBounds), UnityEngine.Random.Range(-YPositionBounds, YPositionBounds), 0f); 

        if (NavMesh.SamplePosition(randomPosition, out hit, 100f, NavMesh.AllAreas))
        {
            return hit.position;
        }

        return transform.position;
    }

    void StopSpawning()
    {
        StopAllCoroutines();
    }
}
