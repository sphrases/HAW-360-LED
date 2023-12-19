using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameTitleCardController : MonoBehaviour
{

    public TextMeshProUGUI textElement;
    public GameObject gameBaseGameObject;
    private Color _startColor = Color.white;


    private void OnTriggerEnter(Collider colliderTarget)
    {
        if (colliderTarget.gameObject.CompareTag("Pointer"))
        {
            textElement.color = Color.red;
        }
    }

    private void OnTriggerExit(Collider colliderTarget)
    {
        textElement.color = _startColor; 
    }

}
