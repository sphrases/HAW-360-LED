using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class MenuLoadingBarController : MonoBehaviour
{
    private Image loadingCircle;
    private FlatscreenPlayerTransformHandler flatScreenPositionHandler;

    void Start()
    {
        loadingCircle = GetComponentInChildren<Image>();
        flatScreenPositionHandler = GetComponent<FlatscreenPlayerTransformHandler>();   
        SetLoadingCircleFillAmount(0f);
    }

    private void StartLoading(CylinderToFlatscreenPosition _interactingPlayer, float _activationTime)
    {
        if(GameManager.Instance.CurrentState != GameManager.States.Playing)
        {
            return;
        }

        flatScreenPositionHandler.ThisPlayersController = _interactingPlayer; // tell the gameObject which controller activated this and will therefore control the gameObject
        flatScreenPositionHandler.SetPlayerControllersCorrespondingInGamePlayer();
        StartCoroutine(LoadingCoroutine(_activationTime));
    }

    private void StopLoading(CylinderToFlatscreenPosition _interactingPlayer)
    {
        StopAllCoroutines();
        SetLoadingCircleFillAmount(0f);
    }

    IEnumerator LoadingCoroutine(float _activationTime)
    {
        float _elapsedTime = 0f;

        while (_elapsedTime < _activationTime)
        {
            SetLoadingCircleFillAmount(Mathf.Lerp(0, 1, _elapsedTime / _activationTime));
            _elapsedTime += Time.deltaTime;
            yield return null;
        }

        SetLoadingCircleFillAmount(0f);
    }

    void SetLoadingCircleFillAmount(float fillAmount)
    {
        loadingCircle.fillAmount = fillAmount;
    }
}
