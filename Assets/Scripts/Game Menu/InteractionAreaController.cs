using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractionAreaController : MonoBehaviour
{
    public UnityEvent<GameObject> StartPauseGameEvent;
    public UnityEvent<GameObject> CancelPauseGameEvent;

    public GameObject MenuGroupParent;

    public static InteractionAreaController Instance;
    public bool RightControllerIsInInteractionArea = false;
    public bool LeftControllerIsInInteractionArea = false;
    public bool RightControllerActivatedInteractionArea = false;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }

        Instance = this;    
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CustomGameControllerLeft") && !RightControllerIsInInteractionArea)
        {
            LeftControllerIsInInteractionArea = true;
        }
        else if (other.CompareTag("CustomGameControllerRight") && !LeftControllerIsInInteractionArea)
        {
            RightControllerIsInInteractionArea = true;
        }

        if (MenuGroupParent.activeSelf)
        {
            return;
        }

        if (other.CompareTag("CustomGameControllerLeft") && !RightControllerIsInInteractionArea)
        {
            RightControllerActivatedInteractionArea = false;
            StartPauseGameEvent?.Invoke(other.gameObject);
        }
        else if (other.CompareTag("CustomGameControllerRight") && !LeftControllerIsInInteractionArea)
        {
            RightControllerActivatedInteractionArea = true;
            StartPauseGameEvent?.Invoke(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CustomGameControllerLeft"))
        {
            LeftControllerIsInInteractionArea = false;
            CancelPauseGameEvent?.Invoke(other.gameObject);
        }
        else if (other.CompareTag("CustomGameControllerRight"))
        {
            RightControllerIsInInteractionArea = false;
            CancelPauseGameEvent?.Invoke(other.gameObject);
        }
    }

}
