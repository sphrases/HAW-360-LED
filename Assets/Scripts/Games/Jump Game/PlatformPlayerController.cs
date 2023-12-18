using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPlayerController : MonoBehaviour
{
    public GameObject PlatformPrefab;
    public Transform PlatformSpawnPosition;
    public Transform PlatformParent;
    public int AvailablePlatforms = 5;

    public event System.Action<int> PlatformAmountChanged;

    private PrimaryButtonWatcher watcher;
    private bool IsPressed = false; // used to display button state in the Unity __Inspector__ window

    private void Start()
    {
        watcher = GetComponent<PrimaryButtonWatcher>();
        watcher.primaryButtonPress.AddListener(onPrimaryButtonEvent);
        PlatformAmountChanged?.Invoke(AvailablePlatforms);
    }

    void Update()
    {
        CheckTriggerButtonPress();
    }

    private void OnTriggerEnter(Collider collider)
    {
        PlatformPickupController _platformPickup = collider.gameObject.GetComponent<PlatformPickupController>();
    
        if(_platformPickup != null)
        {
            AvailablePlatforms += _platformPickup.PlatformAmount;
            PlatformAmountChanged?.Invoke(AvailablePlatforms);
            _platformPickup.DestroyGameObject();
        }
    }

    void CheckTriggerButtonPress()
    {
        if (!IsPressed)
        {
            return;
        }

        PlacePlatform();
        IsPressed = false;
    }

    void PlacePlatform()
    {
        if(AvailablePlatforms <= 0)
        {
            return;
        }

        GameObject _platform = Instantiate(PlatformPrefab, PlatformSpawnPosition.position, Quaternion.identity);
        _platform.transform.SetParent(PlatformParent);
        AvailablePlatforms--;
        PlatformAmountChanged?.Invoke(AvailablePlatforms);
    }

    public void onPrimaryButtonEvent(bool pressed)
    {
        IsPressed = pressed;
    }
}
