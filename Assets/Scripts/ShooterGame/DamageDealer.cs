using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public float Damage = 50f;

    private void OnCollisionEnter(Collision collision)
    {
        HealthController _healthController = collision.gameObject.GetComponent<HealthController>();

        if(_healthController != null)
        {
            _healthController.Health -= Damage;
        }
    }
}
