using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuLoadingBarController : MonoBehaviour
{
    private Image loadingCircle;
    private FlatscreenPlayerTransformHandler flatScreenPositionHandler;

    public float InteractionTime = 3f;

    void Start()
    {
        loadingCircle = GetComponentInChildren<Image>();
        flatScreenPositionHandler = GetComponent<FlatscreenPlayerTransformHandler>();   
        SetLoadingCircleFillAmount(0f);
        InteractionAreaController.Instance.InteractionStarted += StartLoading;
        InteractionAreaController.Instance.InteractionAborted += StopLoading;
    }

    private void StartLoading(CylinderToFlatscreenPosition _interactingPlayer)
    {
        if(GameManager.Instance.ThisGameState.CurrentState != (int)GameState.States.Playing)
        {
            return;
        }

        flatScreenPositionHandler.ThisPlayersController = _interactingPlayer; // tell the gameObject which controller activated this and will therefore control the gameObject
        flatScreenPositionHandler.SetPlayerControllersCorrespondingInGamePlayer();
        StartCoroutine(LoadingCoroutine());
    }

    private void StopLoading(CylinderToFlatscreenPosition _interactingPlayer)
    {
        StopAllCoroutines();
        SetLoadingCircleFillAmount(0f);
    }

    IEnumerator LoadingCoroutine()
    {
        float _elapsedTime = 0f;

        while (_elapsedTime < InteractionTime)
        {
            SetLoadingCircleFillAmount(Mathf.Lerp(0, 1, _elapsedTime / InteractionTime));
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
