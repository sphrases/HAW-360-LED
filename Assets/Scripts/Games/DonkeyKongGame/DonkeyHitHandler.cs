using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class DonkeyHitHandler : MonoBehaviour
{
    public float PlayerHitVisualizationTime = 2f;
    public float PlayerHitBlinkTime = 0.4f;
    public AudioSource PlayerHitSound;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("DonkeyBall"))
        {
            PlayerHitSound.Play();
            StopAllCoroutines();
            StartCoroutine(PlayerHitVisualizationCoroutine());
        }
    }

    IEnumerator PlayerHitVisualizationCoroutine()
    {
        float _elapsedTime = 0f;

        while (_elapsedTime < PlayerHitVisualizationTime)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            _elapsedTime += PlayerHitBlinkTime;
            yield return new WaitForSeconds(PlayerHitBlinkTime);
        }

        spriteRenderer.enabled = true;
    }
}
