using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractionAreaController : MonoBehaviour
{
    public UnityEvent<GameObject> StartPauseGameEvent;
    public UnityEvent<GameObject> CancelPauseGameEvent;


    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("CustomGameControllerLeft") || other.CompareTag("CustomGameControllerRight"))
        {
            StartPauseGameEvent?.Invoke(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CustomGameControllerLeft") || other.CompareTag("CustomGameControllerRight"))
        {
            CancelPauseGameEvent?.Invoke(other.gameObject);
        }
    }

}
