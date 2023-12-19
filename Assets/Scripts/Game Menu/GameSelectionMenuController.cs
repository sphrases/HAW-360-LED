using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameSelectionMenuController : MonoBehaviour
{
    public RectTransform CalculationCanvas;
    public List<GameObject> gameTitleCards; // This needs to be GameObject as type, but the gameobjects must have a child "GameTitleCard" instance

    private float _canvasHeightNormalized;
    private float _offsetYAxisPadding = 2.5f;
    private float _offsetYAxisElementHeight = 1;
    private float _offsetYAxis = 0;



    void Awake()
    {
        // Initialize pre-render
        _canvasHeightNormalized = CalculationCanvas.rect.height / 2;

        // Debug.Log("_canvasHeigh: " + _canvasHeightNormalized + "   width: " + _canvasWidthNormalized);

        foreach (GameObject gTitle in gameTitleCards)
        {
            Transform positionOffset = this.transform;
            positionOffset.position = new Vector3(this.transform.position.x, _canvasHeightNormalized - _offsetYAxis - _offsetYAxisElementHeight, this.transform.position.z);
            // Debug.Log(positionOffset.position);
            Instantiate(gTitle, positionOffset.position, positionOffset.rotation);
            _offsetYAxis += _offsetYAxisPadding;
        }

    }


    // This is an Event Callback from the pointer controller. 
    // It receives the GameBaseClass that is hovered over (After the cooldown, so the selection has been made)
    // Each GameBaseClass has start- and stop-game events.
    // So call stop on each (besides the passed one) and start vice-versa

    public void StartGameTriggered(GameTitleCardController gTC)
    {

        // Debug.Log("Started Game   " + gTC.gameBaseClass.gameTitle);
        GameBaseClass[] foundGameClasses = FindObjectsByType<GameBaseClass>(FindObjectsSortMode.None);

        // Kill all games
        foreach (GameBaseClass gameBaseClass in foundGameClasses)
        {
            Destroy(gameBaseClass.gameObject);
        }




        // start selected game
        Instantiate(gTC.gameBaseGameObject, new Vector3(0, 0, 14.46805f), transform.rotation);

    }
}
