using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    public float PlayerHitVisualizationTime = 2f;
    public float PlayerHitBlinkTime = 0.4f;
    public GameObject MeshObject;

    private PlayerPowerUpHandler powerUpHandler;

    private void Start()
    {
        powerUpHandler = GetComponent<PlayerPowerUpHandler>();
        GameManager.Instance.GameMenuActivated += ActivateMesh;
    }

    private void OnTriggerEnter(Collider other)
    {
        bool _wasMeteorite = CheckForMeteorite(other);
        
        if(_wasMeteorite)
        {
            return;
        }

        CheckForEnemyShip(other);
    }

    bool CheckForMeteorite(Collider other)
    {
        MeteoriteController _meteoriteController = other.GetComponent<MeteoriteController>();

        if (_meteoriteController == null)
        {
            return false;
        }

        _meteoriteController.DestroyGameObject();

        if(powerUpHandler.CurrentPowerUp == PlayerPowerUpHandler.PowerUps.Shield)
        {
            powerUpHandler.DeactivateShield();
            return true;
        }

        MeshObject.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(PlayerHitVisualizationCoroutine());
        return true;
    }

    void CheckForEnemyShip(Collider other)
    {
        EnemyShipController _enemyShipController = other.GetComponent<EnemyShipController>();

        if (_enemyShipController == null)
        {
            return;
        }

        _enemyShipController.DestroyGameObject();

        if (powerUpHandler.CurrentPowerUp == PlayerPowerUpHandler.PowerUps.Shield)
        {
            powerUpHandler.DeactivateShield();
            return;
        }

        MeshObject.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(PlayerHitVisualizationCoroutine());
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
