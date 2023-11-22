using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerProjectileHandler : MonoBehaviour
{
    public PickupObject.PickupType CurrentProjectileType = PickupObject.PickupType.Bullet;
    public GameObject Bullet;
    public GameObject Rocket;
    public int MaxProjectileAmount = 3;
    public Transform ProjectileSpawnPosition;
    public float PushBackForce = 1.0f;

    public event System.Action<int, PickupObject.PickupType> ProjectilesChanged;

    private PrimaryButtonWatcher watcher;
    private Rigidbody rb;
    private int currentProjectileAmount;
    private bool IsPressed = false; // used to display button state in the Unity __Inspector__ window

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        watcher = GetComponent<PrimaryButtonWatcher>();
        watcher.primaryButtonPress.AddListener(onPrimaryButtonEvent);
        currentProjectileAmount = MaxProjectileAmount;
        ProjectilesChanged?.Invoke(currentProjectileAmount, CurrentProjectileType);
    }

    void Update()
    {
        CheckTriggerButtonPress();
    }

    void CheckTriggerButtonPress()
    {
        if (!IsPressed)
        {
            return;
        }

        if(currentProjectileAmount <= 0)
        {
            return;
        }

        Shoot();
        IsPressed = false;
    }

    void Shoot()
    {
        switch(CurrentProjectileType)
        {
            case PickupObject.PickupType.Mine:
                currentProjectileAmount--;
                break;
            case PickupObject.PickupType.Bullet:
                Instantiate(Bullet, ProjectileSpawnPosition.position, transform.rotation);
                currentProjectileAmount--;
                break;
            case PickupObject.PickupType.Rocket:
                GameObject _rocket = Instantiate(Rocket, ProjectileSpawnPosition.position, transform.rotation);
                _rocket.GetComponent<RocketMovementController>().SetDestination(gameObject);
                currentProjectileAmount--;
                break;
            default:
                break;
        }

        ProjectilesChanged?.Invoke(currentProjectileAmount, CurrentProjectileType);
        Vector3 _pushback = PushBackForce * (ProjectileSpawnPosition.position - transform.position).normalized;
        rb.AddForce(_pushback);
    }

    private void OnTriggerEnter(Collider collision)
    {
        PickupObject projectilePickup = collision.GetComponent<PickupObject>();

        if (projectilePickup != null)
        {
            CurrentProjectileType = projectilePickup.ThisPickupType;
            currentProjectileAmount = MaxProjectileAmount;
            ProjectilesChanged?.Invoke(currentProjectileAmount, CurrentProjectileType);
            projectilePickup.DestroyGameObject();
        }
    }
    public void onPrimaryButtonEvent(bool pressed)
    {
        IsPressed = pressed;
    }
}
