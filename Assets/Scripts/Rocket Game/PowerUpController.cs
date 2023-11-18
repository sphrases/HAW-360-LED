using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    public PlayerPowerUpHandler.PowerUps PowerUpType;

    public void DestroyPowerUp()
    {
        Destroy(gameObject);
    }
}
