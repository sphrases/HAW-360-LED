using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : MonoBehaviour
{
    public enum PickupType
    {
        Uninitialized,
        Bullet,
        Mine,
        Rocket
    }

    public GameObject BulletIcon;
    public GameObject MineIcon;
    public GameObject RocketIcon;
    public PickupType ThisPickupType;

    private void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        BulletIcon.SetActive(false);
        MineIcon.SetActive(false);
        RocketIcon.SetActive(false);

        switch(ThisPickupType)
        {
            case PickupType.Bullet:
                BulletIcon.SetActive(true);
                break;
            case PickupType.Mine:
                MineIcon.SetActive(true);
                break;
            case PickupType.Rocket:
                RocketIcon.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void DestroyGameObject()
    {
        PickupSpawner.Instance.ReduceSpawnedElements();
        Destroy(gameObject);
    }
}
