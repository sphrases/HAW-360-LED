using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ShootHandler : MonoBehaviour
{
    public GameObject Bullet;
    public Transform BulletSpawnPosition;

    private PrimaryButtonWatcher watcher;
    private bool IsPressed = false; // used to display button state in the Unity __Inspector__ window

    private void Start()
    {
        watcher = GetComponent<PrimaryButtonWatcher>();
        watcher.primaryButtonPress.AddListener(onPrimaryButtonEvent);
    }

    void Update()
    {
        CheckTriggerButtonPress();
    }

    void CheckTriggerButtonPress()
    {
        if (IsPressed)
        {
            Shoot();
            IsPressed = false;
        }
    }

    void Shoot()
    {
        Instantiate(Bullet, BulletSpawnPosition.position, transform.rotation);
    }
    public void onPrimaryButtonEvent(bool pressed)
    {
        IsPressed = pressed;
    }
}
