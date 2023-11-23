using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public enum Games
    {
        RocketGame,
        PongGame,
        ShooterGame,
        JumpGame
    }

    public enum States
    {
        Playing,
        Menu,
        GameSelectionMenu
    }

    public GameManager.Games ActiveGame = Games.RocketGame;
    public GameObject PongGame;
    public GameObject MeteoriteGame;
    public GameObject ShooterGame;
    public GameObject JumpGame;
    public GameObject GameMenu;
    public GameObject ButtonParent;
    public GameMenuButtonHandler ChangeGameButton;
    public GameMenuButtonHandler RestartButton;
    public GameMenuButtonHandler ContinueButton;
    public GameSelectionMenuController GameSelectionMenu;
    public FlatscreenPlayerTransformHandler GameMenuPointer;
    public FlatscreenPlayerTransformHandler SelectionMenuPointer;
    public float MenuOffsetToPlayer = 1.5f;
    public States CurrentState;

    public event System.Action GameMenuActivated;
    public event System.Action GameMenuDeactivated;

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        InteractionAreaController.Instance.InteractionCompleted += ActivateGameMenu;
        ContinueButton.ButtonActivated += DeactivateGameMenu;
        ChangeGameButton.ButtonActivated += ActivateGameSelectionMenu;
        GameSelectionMenu.StartGame += StartGame;
        InitializeGame();
    }

    void ActivateGameMenu(CylinderToFlatscreenPosition _interactingPlayer)
    {
        CurrentState = States.Menu;
        GameMenuActivated?.Invoke();
        GameMenu.SetActive(true);
        ButtonParent.transform.position = new Vector3(_interactingPlayer.CorrespondingInGamePlayer.transform.position.x, _interactingPlayer.CorrespondingInGamePlayer.transform.position.y + MenuOffsetToPlayer, transform.position.z);
        GameMenuPointer.ThisPlayersController = _interactingPlayer; // tell the gameObject which controller activated this and will therefore control the gameObject
        GameMenuPointer.SetPlayerControllersCorrespondingInGamePlayer();
    }

    void DeactivateGameMenu()
    {
        GameMenuDeactivated?.Invoke();
        GameMenu.SetActive(false);
        CurrentState = States.Playing;
    }

    void ActivateGameSelectionMenu()
    {
        DeactivateGameMenu();
        CurrentState = States.GameSelectionMenu;
        PongGame.SetActive(false);
        MeteoriteGame.SetActive(false);
        JumpGame.SetActive(false);
        ShooterGame.SetActive(false);
        GameSelectionMenu.gameObject.SetActive(true);
        SelectionMenuPointer.ThisPlayersController = GameMenuPointer.ThisPlayersController;
        SelectionMenuPointer.SetPlayerControllersCorrespondingInGamePlayer();
    }

    void DeactivateGameSelectionMenu()
    {
        GameSelectionMenu.gameObject.SetActive(false);
        CurrentState = States.Playing;
    }

    void StartGame(Games _game)
    {
        ActiveGame = _game;
        DeactivateGameSelectionMenu();
        DeactivateGameMenu();
        InitializeGame();
        CurrentState = States.Playing;
    }

    void InitializeGame()
    {
        PongGame.SetActive(false);
        MeteoriteGame.SetActive(false);
        JumpGame.SetActive(false);
        ShooterGame.SetActive(false);
        DeactivateGameMenu();
        DeactivateGameSelectionMenu();
        CurrentState = States.Playing;

        switch (ActiveGame)
        {
            case Games.RocketGame:
                MeteoriteGame.SetActive(true);
                break;
            case Games.PongGame:
                PongGame.SetActive(true);
                break;
            case Games.ShooterGame:
                ShooterGame.SetActive(true);
                break;
            case Games.JumpGame:
                JumpGame.SetActive(true);
                break;
            default:
                break;
        }
    }

    private void OnDestroy()
    {
        InteractionAreaController.Instance.InteractionCompleted -= ActivateGameMenu;
        ContinueButton.ButtonActivated -= DeactivateGameMenu;
        ChangeGameButton.ButtonActivated -= ActivateGameSelectionMenu;
    }
}