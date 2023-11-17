using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionAreaController : MonoBehaviour
{
    public float ActivationTime = 3f;
    public event System.Action<CylinderToFlatscreenPosition> Interaction;

    public static InteractionAreaController Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("GameController"))
        {
            CylinderToFlatscreenPosition _interactingPlayer = other.GetComponent<CylinderToFlatscreenPosition>();
            StartCoroutine(InteractionActivationCoroutine(_interactingPlayer));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("GameController"))
        {
            StopAllCoroutines();
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

        Interaction.Invoke(_interactingPlayer);
    }
}
