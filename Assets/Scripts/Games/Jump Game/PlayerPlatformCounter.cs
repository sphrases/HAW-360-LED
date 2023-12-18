using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformCounter : MonoBehaviour
{
    public GameObject PlatformSymbolPrefab;
    public float PlatformSymbolDistance = 0.2f;

    private PlatformPlayerController playerController;
    private List<GameObject> platformSymbols = new List<GameObject>();

    private void Awake()
    {
        GetComponent<PlatformPlayerController>().PlatformAmountChanged += UpdatePlatforms;
    }

    void UpdatePlatforms(int _platformCount)
    {
        if (_platformCount > platformSymbols.Count)
        {
            while(_platformCount > platformSymbols.Count)
            {
                GameObject _platformSymbol;
                Vector3 _spawnPosition;

                if (platformSymbols.Count > 0)
                {
                    _spawnPosition = new Vector3(transform.position.x, platformSymbols[platformSymbols.Count - 1].transform.position.y + PlatformSymbolDistance, transform.position.z);
                }
                else
                {
                    _spawnPosition = new Vector3(transform.position.x, PlatformSymbolDistance, transform.position.z);
                }

                _platformSymbol = Instantiate(PlatformSymbolPrefab, _spawnPosition, transform.rotation);
                _platformSymbol.transform.SetParent(transform);
                platformSymbols.Add(_platformSymbol);
            }
        }
        else if (_platformCount < platformSymbols.Count)
        {
            while (_platformCount < platformSymbols.Count)
            {
                Destroy(platformSymbols[platformSymbols.Count - 1]);
                platformSymbols.RemoveAt(platformSymbols.Count - 1);
            }
        }
    }

    private void OnDestroy()
    {
        GetComponent<PlatformPlayerController>().PlatformAmountChanged -= UpdatePlatforms;
    }

    // detect collision with platformPickup
    // get platform amount and update platforms
    // set parent of platform symbols to transform
}
