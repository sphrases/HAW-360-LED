using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    private float health = 100f;

    public float Health
    { 
        get 
        { 
            return health; 
        } 
        set 
        {
            health = value; 

            if(health <= 0f)
            {
                Die();
            }
        } 
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
