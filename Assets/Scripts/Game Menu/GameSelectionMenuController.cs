using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameSelectionMenuController : MonoBehaviour
{
    public RectTransform CalculationCanvas;
    public List<GameObject> gameTitleCards; // This needs to be GameObject as type, but the gameobjects must have a child "GameTitleCard" instance
    public GameObject GameGroupParent;
    public GameObject MenuGroupParent;
    public GameObject Pointer;
    public int startGameIndex = 0;



    private float _canvasHeightNormalized;
    private float _offsetYAxisPadding = 2.5f;
    private float _offsetYAxisElementHeight = 1;
    private float _offsetYAxis = 0;



    private void Awake()
    {
        // Initialize pre-render
        _canvasHeightNormalized = CalculationCanvas.rect.height / 2;

        // Debug.Log("_canvasHeigh: " + _canvasHeightNormalized + "   width: " + _canvasWidthNormalized);

        foreach (GameObject gTitle in gameTitleCards)
        {
            Transform positionOffset = MenuGroupParent.transform;

            GameObject instance = Instantiate(gTitle, MenuGroupParent.transform);

            instance.transform.localPosition = new Vector3(0, _canvasHeightNormalized - _offsetYAxis - _offsetYAxisElementHeight, 0);

            _offsetYAxis += _offsetYAxisPadding;
        }


        // Activate first game 
        if (gameTitleCards[startGameIndex])
        {
            Instantiate(gameTitleCards[startGameIndex].gameObject.GetComponentInChildren<GameTitleCardController>().gameBaseGameObject, GameGroupParent.transform.position, GameGroupParent.transform.rotation, GameGroupParent.transform);
        }

    }

    public void Start()
    {
        HideGameSelectionMenu();
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
        Instantiate(gTC.gameBaseGameObject, GameGroupParent.transform.position, GameGroupParent.transform.rotation, GameGroupParent.transform);
        HideGameSelectionMenu();
        HidePointer();
    }


    public void ShowGameSelectionMenu()
    {
        this.MenuGroupParent.SetActive(true);
    }

    public void HideGameSelectionMenu()
    {
        this.MenuGroupParent.SetActive(false);
    }

    public void ShowPointer()
    {
        this.Pointer.SetActive(true);
    }

    public void HidePointer()
    {
        this.Pointer.SetActive(false);
    }


    public void StartPauseGameEventHandler(GameObject emitter)
    {
        ShowPointer();

        Pointer.GetComponent<FlatscreenPlayerTransformHandler>().SetPlayersController(emitter.GetComponent<CylinderToFlatscreenPosition>());
        Pointer.GetComponent<MenuPointerController>().StartLoadingIndicator(() =>
        {
            ShowGameSelectionMenu();

        });

    }
    public void CancelPauseGameEventHandler(GameObject emitter)
    {
        Pointer.GetComponent<MenuPointerController>().CancelLoadingIndicator();
        HidePointer();
        HideGameSelectionMenu();
    }

}
