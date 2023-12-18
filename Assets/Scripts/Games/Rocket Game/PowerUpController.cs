using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    public PlayerPowerUpHandler.PowerUps PowerUpType;
    public float Lifetime = 1f;
    public float DestroySoonVisualizationTime = 2f;
    public float DestroySoonBlinkTime = 0.2f;
    public GameObject MeshObject;

    public void DestroyPowerUp()
    {
        Destroy(gameObject);
    }

    public void StartDestroyCountdown()
    {
        StartCoroutine(DestroyCountdownCoroutine());
    }

    IEnumerator DestroyCountdownCoroutine()
    {
        yield return new WaitForSeconds(Lifetime - DestroySoonVisualizationTime);
        StartCoroutine(DestroySoonVisualizationCoroutine());
    }

    IEnumerator DestroySoonVisualizationCoroutine()
    {
        float _elapsedTime = 0f;

        while (_elapsedTime < DestroySoonVisualizationTime)
        {
            MeshObject.SetActive(!MeshObject.activeSelf);
            _elapsedTime += DestroySoonBlinkTime;
            yield return new WaitForSeconds(DestroySoonBlinkTime);
        }

        MeshObject.SetActive(true);
        Destroy(gameObject);
    }
}
