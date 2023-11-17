using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameMenuButtonHandler : MonoBehaviour
{ 
    public Color HighlightedColor; // Color for highlighted state
    public Color NormalColor; // Store the normal color of the button
    public float InteractionTime = 3f;

    public event System.Action ButtonActivated;

    private SpriteRenderer spriteRenderer; // Reference to your button
    private Image LoadingCircle;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        LoadingCircle = GetComponentInChildren<Image>();
        SetLoadingCircleFillAmount(0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Pointer"))
        {
            spriteRenderer.color = HighlightedColor;
            StartCoroutine(LoadingCoroutine());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        StopAllCoroutines();
        spriteRenderer.color = NormalColor;
        SetLoadingCircleFillAmount(0f);
    }

    IEnumerator LoadingCoroutine()
    {
        float _elapsedTime = 0f;

        while(_elapsedTime < InteractionTime)
        {
            SetLoadingCircleFillAmount(Mathf.Lerp(0, 1, _elapsedTime/InteractionTime));
            _elapsedTime += Time.deltaTime;
            yield return null;
        }

        SetLoadingCircleFillAmount(0f);
        spriteRenderer.color = NormalColor;
        ButtonActivated?.Invoke();
    }

    void SetLoadingCircleFillAmount(float fillAmount)
    {
        LoadingCircle.fillAmount = fillAmount;
    }
}
