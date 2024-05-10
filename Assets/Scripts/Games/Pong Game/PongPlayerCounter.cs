using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PongPlayerCounter : MonoBehaviour
{
    private int currentPoints = 0;
    public TextMeshProUGUI counterText;

    public void IncreaseCounter()
    {
        currentPoints++;
        counterText.text = currentPoints.ToString();
    }
}
