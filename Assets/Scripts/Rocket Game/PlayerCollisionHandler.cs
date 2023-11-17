using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    public float PlayerHitVisualizationTime = 2f;
    public float PlayerHitBlinkTime = 0.4f;
    public GameObject MeshObject;

    private void Start()
    {
        GameManager.Instance.GameMenuActivated += ActivateMesh;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Meteorite"))
        {
            MeshObject.SetActive(true);
            StopAllCoroutines();
            StartCoroutine(PlayerHitVisualizationCoroutine());
        }
    }

    IEnumerator PlayerHitVisualizationCoroutine()
    {
        float _elapsedTime = 0f;

        while(_elapsedTime < PlayerHitVisualizationTime)
        {
            MeshObject.SetActive(!MeshObject.activeSelf);
            _elapsedTime += PlayerHitBlinkTime;
            yield return new WaitForSeconds(PlayerHitBlinkTime);
        }

        MeshObject.SetActive(true);
    }

    void ActivateMesh()
    {
        MeshObject.SetActive(true);
    }

    private void OnDestroy()
    {
        GameManager.Instance.GameMenuActivated -= ActivateMesh;
    }
}
