using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameMenuButtonHandler : MonoBehaviour
{
    private Button button; // Reference to your button

    public Color HighlightedColor; // Color for highlighted state
    public Color PressedColor; // Color for pressed state
    public Color normalColor; // Store the normal color of the button

    void Start()
    {
        button = GetComponent<Button>();
        normalColor = button.colors.normalColor; // Get the normal color of the button
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
