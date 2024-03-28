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


    private List<GameObject> instantiatedGames = new List<GameObject>();
    private float _canvasHeightNormalized;
    public float _offsetYAxisPadding = 2.5f;
    public float _offsetYAxisElementHeight = 1;
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
            GameObject gameInstance = Instantiate(gameTitleCards[startGameIndex].gameObject.GetComponentInChildren<GameTitleCardController>().gameBaseGameObject, GameGroupParent.transform.position, GameGroupParent.transform.rotation, GameGroupParent.transform);
            instantiatedGames.Add(gameInstance);
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
        // start selected game
        GameObject gameInstance = Instantiate(gTC.gameBaseGameObject, GameGroupParent.transform.position, GameGroupParent.transform.rotation, GameGroupParent.transform);
        instantiatedGames.Add(gameInstance);
        HideGameSelectionMenu();
        HidePointer();
    }


    public void ShowGameSelectionMenu()
    {
        this.MenuGroupParent.SetActive(true);

        foreach(GameObject gameInstance in instantiatedGames)
        {
            Destroy(gameInstance);
        }
    }

    public void HideGameSelectionMenu()
    {
        Debug.LogError("hidden");
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
        if(MenuGroupParent.activeSelf)
        {
            return; // to prevent quitting game menu without selecting a game because i have no idea where thats actually called from so i cant remove the call
        }

        Pointer.GetComponent<MenuPointerController>().CancelLoadingIndicator();
        HidePointer();
        Debug.LogError("canceled");
        HideGameSelectionMenu();
    }

}
