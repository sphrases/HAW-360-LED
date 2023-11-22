using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class AmmoIconController : MonoBehaviour
{
    public GameObject RocketIcon;
    public GameObject BulletIcon;
    public GameObject MineIcon;

    public void SwitchIcon(PickupObject.PickupType _currentProjectileType)
    {
        MineIcon.SetActive(false);
        BulletIcon.SetActive(false);
        RocketIcon.SetActive(false);

        switch (_currentProjectileType)
        {
            case PickupObject.PickupType.Mine:
                MineIcon.SetActive(true);
                break;
            case PickupObject.PickupType.Bullet:
                BulletIcon.SetActive(true);
                break;
            case PickupObject.PickupType.Rocket:
                RocketIcon.SetActive(true);
                break;
            default:
                break;
        }
    }
}
