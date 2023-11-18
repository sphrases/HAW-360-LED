using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerUpHandler : MonoBehaviour
{
    public enum PowerUps
    {
        None,
        Shield
    }

    public PowerUps CurrentPowerUp;
    public GameObject Shield;

    private void OnTriggerEnter(Collider other)
    {
        CheckForPowerUp(other);
    }

    void CheckForPowerUp(Collider other)
    {
        PowerUpController _powerUpController = other.GetComponent<PowerUpController>();

        if (_powerUpController == null)
        {
            return;
        }

        PowerUps _powerUpType = _powerUpController.PowerUpType;

        switch (_powerUpType)
        {
            case PowerUps.None:
                CurrentPowerUp = PowerUps.None;
                break;
            case PowerUps.Shield:
                Shield.SetActive(true);
                CurrentPowerUp = PowerUps.Shield;
                break;
        }

        _powerUpController.DestroyPowerUp();
    }

    public void DeactivateShield()
    {
        Shield.SetActive(false);
        CurrentPowerUp = PowerUps.None;
    }
}
