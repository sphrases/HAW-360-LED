using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorButton : MonoBehaviour
{
    public event System.Action ButtonActivated;

    private void OnTriggerEnter(Collider other)
    {
        ButtonActivated?.Invoke();
    }
}
