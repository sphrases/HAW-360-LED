using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public float Damage = 50f;

    private bool detectCollisions = true;

    private void OnCollisionEnter(Collision collision)
    {
        HealthController _healthController = collision.gameObject.GetComponent<HealthController>();

        if(_healthController == null)
        {
            return;
        }

        if (!detectCollisions)
        {
            return;
        }

        detectCollisions = false;
        _healthController.Health -= Damage;
        Destroy(gameObject);
    }
}
