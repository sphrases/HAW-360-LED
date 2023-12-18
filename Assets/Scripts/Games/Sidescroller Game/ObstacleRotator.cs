using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleRotator : MonoBehaviour
{
    public List<RotatorButton> rotatorButtons;
    public float RotationDuration = 0.5f;

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        SubscribeToButtons();
    }

    void SubscribeToButtons()
    {
        foreach (var button in rotatorButtons)
        {
            button.ButtonActivated += Rotate;
        }
    }

    void Rotate()
    {
        StartCoroutine(RotateOverTime());
    }

    IEnumerator RotateOverTime()
    {
        float elapsedTime = 0f;
        Quaternion startRotation = rb.rotation;
        Quaternion targetRotation = startRotation * Quaternion.Euler(0f, 0f, 90f); 

        while (elapsedTime < RotationDuration)
        {
            rb.MoveRotation(Quaternion.Slerp(startRotation, targetRotation, elapsedTime / RotationDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rb.MoveRotation(targetRotation);
    }

    private void OnDestroy()
    {
        foreach (var button in rotatorButtons)
        {
            button.ButtonActivated -= Rotate;
        }
    }
}
