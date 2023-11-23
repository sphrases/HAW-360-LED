using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionAreaController : MonoBehaviour
{
    public float ActivationTime = 3f;
    public event System.Action<CylinderToFlatscreenPosition> InteractionCompleted;
    public event System.Action<CylinderToFlatscreenPosition, float> InteractionStarted;
    public event System.Action<CylinderToFlatscreenPosition> InteractionAborted;

    public static InteractionAreaController Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(GameManager.Instance.CurrentState != GameManager.States.Playing)
        {
            return;
        }

        if(other.CompareTag("GameController"))
        {
            CylinderToFlatscreenPosition _interactingPlayer = other.GetComponent<CylinderToFlatscreenPosition>();
            InteractionStarted?.Invoke(_interactingPlayer, ActivationTime);
            StartCoroutine(InteractionActivationCoroutine(_interactingPlayer));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("GameController"))
        {
            StopAllCoroutines();
            CylinderToFlatscreenPosition _interactingPlayer = other.GetComponent<CylinderToFlatscreenPosition>();
            InteractionAborted?.Invoke(_interactingPlayer);
        }
    }

    private IEnumerator InteractionActivationCoroutine(CylinderToFlatscreenPosition _interactingPlayer)
    {
        float _elapsedTime = 0f;

        while(_elapsedTime < ActivationTime)
        {
            _elapsedTime += Time.deltaTime;
            yield return null;
        }

        InteractionCompleted?.Invoke(_interactingPlayer);
    }
}
