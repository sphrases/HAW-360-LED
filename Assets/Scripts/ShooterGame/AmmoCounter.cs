using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCounter : MonoBehaviour
{
    public List<GameObject> Ammo;

    private PlayerProjectileHandler projectileHandler;
    PickupObject.PickupType thisCurrentType = PickupObject.PickupType.Uninitialized;

    private void Awake()
    {
        projectileHandler = transform.parent.GetComponentInChildren<PlayerProjectileHandler>();
        projectileHandler.ProjectilesChanged += UpdateAmmoCounter;
    }

    void UpdateAmmoCounter(int currentProjectileAmount, PickupObject.PickupType currentType)
    {
        if(thisCurrentType != currentType)
        {
            for (int i = 0; i < currentProjectileAmount; i++)
            {
                Ammo[i].SetActive(true);
                Ammo[i].GetComponent<AmmoIconController>().SwitchIcon(currentType);
            }
        }

        for(int i = 0; i < currentProjectileAmount; i++)
        {
            Ammo[i].SetActive(true);
        }

        for(int i = currentProjectileAmount; i >= currentProjectileAmount && i < Ammo.Count; i++)
        {
            Ammo[i].SetActive(false);
        }
    }

    private void OnDestroy()
    {
        projectileHandler.ProjectilesChanged -= UpdateAmmoCounter;
    }
}
