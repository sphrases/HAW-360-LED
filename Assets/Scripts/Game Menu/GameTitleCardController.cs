using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameTitleCardController : MonoBehaviour
{

    public List<TextMeshProUGUI> textElements;
    public GameObject gameBaseGameObject;
    private Color _startColor = Color.white;


    private void OnTriggerEnter(Collider colliderTarget)
    {
        if (colliderTarget.gameObject.CompareTag("Pointer"))
        {
            foreach(TextMeshProUGUI text in textElements)
            {
                text.color = Color.red;
            }
        }
    }

    private void OnTriggerExit(Collider colliderTarget)
    {
        foreach (TextMeshProUGUI text in textElements)
        {
            text.color = _startColor;
        }
    }
}
