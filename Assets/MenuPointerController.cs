using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MenuPointerController : MonoBehaviour
{
    public UnityEvent<GameTitleCardController> handleStartGame;
    public float InteractionTime = 3f;

    private Image _loadingCircle;
    private GameTitleCardController _justCollidedWith;


    void Start()
    {
        _loadingCircle = GetComponentInChildren<Image>();
        SetLoadingCircleFillAmount(0f);
    }





    private void OnTriggerEnter(Collider colliderTarget)
    {
        if (colliderTarget.gameObject.CompareTag("GameTitleCard"))
        {
            // If MenuPointer is colliding with a GameTitleCard, start countdown. 
            // If countdown finishes, call handleStartGame on the GameSelectionController;

            GameTitleCardController controller = colliderTarget.gameObject.GetComponent<GameTitleCardController>();
            // Debug.Log("Collision filtered: " + controller.gameBaseClass.gameTitle);
            _justCollidedWith = controller;


            // Start loading Coroutine
            StartLoadingIndicator(ExecuteStartGame);
        }
    }


    public void StartLoadingIndicator(Action callback)
    {

        StartCoroutine(LoadingCoroutine(callback));
    }


    public void CancelLoadingIndicator()
    {
        StopAllCoroutines();
        SetLoadingCircleFillAmount(0f);
    }


    private void OnTriggerExit(Collider colliderTarget)
    {
        if (colliderTarget.gameObject.CompareTag("GameTitleCard"))
        {
            CancelLoadingIndicator();
            _justCollidedWith = null;
        }

    }

    public IEnumerator LoadingCoroutine(Action callback)
    {
        float _elapsedTime = 0f;

        while (_elapsedTime < InteractionTime)
        {
            SetLoadingCircleFillAmount(Mathf.Lerp(0, 1, _elapsedTime / InteractionTime));
            _elapsedTime += Time.deltaTime;
            yield return null;
        }

        SetLoadingCircleFillAmount(0f);
        callback.Invoke();
    }

    private void SetLoadingCircleFillAmount(float fillAmount)
    {
        _loadingCircle.fillAmount = fillAmount;
    }


    private void ExecuteStartGame()
    {
        handleStartGame.Invoke(_justCollidedWith);
    }

}
