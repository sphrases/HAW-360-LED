using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameMenuButtonHandler : MonoBehaviour
{
    private Button button; // Reference to your button

    public Color HighlightedColor; // Color for highlighted state
    public Color PressedColor; // Color for pressed state
    public Color NormalColor; // Store the normal color of the button

    void Start()
    {
        button = GetComponent<Button>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("entered");
        if(other.CompareTag("Pointer"))
        {
            button.image.color = HighlightedColor;
            StartCoroutine(WaitForButtonPressCoroutine());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        StopAllCoroutines();
        button.image.color = NormalColor;
    }

    IEnumerator WaitForButtonPressCoroutine()
    {
        while(true)
        {
            //if button pressed, do something and change to pressed color
            yield return null;
        }
    }
}
