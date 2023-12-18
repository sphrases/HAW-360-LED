using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DistanceCounter : MonoBehaviour
{
    public TextMeshProUGUI DistanceText;

    private int distance = 0;

    void Update()
    {
        distance++;
        DistanceText.text = distance.ToString() + " m";
    }
}
