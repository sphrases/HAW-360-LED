using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public float Damage = 50f;

    //private bool detectCollisions = true;

    public void DamageOtherPlayer(Collider other)
    {
        //HealthController _healthController = other.gameObject.GetComponent<HealthController>();

        //if(_healthController == null)
        //{
        //    return;
        //}

        //if (!detectCollisions)
        //{
        //    return;
        //}

        //detectCollisions = false;
        //_healthController.Health -= Damage;
        //Destroy(gameObject);
    }
}
