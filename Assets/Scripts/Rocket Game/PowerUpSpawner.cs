using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public float SpawnInterval = 15f;
    public float MaxXValue = 9.6f;
    public List<GameObject> PowerUps;

    private bool isSpawning = false;

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
        if(!isSpawning)
        {
            StartCoroutine(SpawnCoroutine());
        }
    }
    void StopSpawning()
    {
        StopAllCoroutines();
        isSpawning = false;
    }

    IEnumerator SpawnCoroutine()
    {
        isSpawning = true;

        while (true)
        {
            SpawnPowerUpRandomly();
            yield return new WaitForSeconds(SpawnInterval);
        }
    }

    void SpawnPowerUpRandomly()
    {
        Vector3 _spawnPosition = new Vector3(Random.Range(-MaxXValue, MaxXValue), transform.position.y, transform.position.z);
        int _randomPowerUpPosition = Random.Range(0, PowerUps.Count - 1);
        GameObject _powerUp = Instantiate(PowerUps[_randomPowerUpPosition], _spawnPosition, transform.rotation);
        _powerUp.transform.parent = transform;
        PowerUpController _powerUpController = _powerUp.GetComponent<PowerUpController>();
        _powerUpController.Lifetime = SpawnInterval;
        _powerUpController.StartDestroyCountdown();
    }
}
