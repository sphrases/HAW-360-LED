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
        JumpGame,
        BounceGame,
        SidescrollerGame,
        DonkeyKongGame,
        HunterGame
    }

    public enum States
    {
        Playing,
        Menu,
        GameSelectionMenu
    }

    public GameManager.Games ActiveGame = Games.RocketGame;
    // public GameObject PongGame;
    public GameObject MeteoriteGame;
    public GameObject ShooterGame;
    public GameObject JumpGame;
    public GameObject BounceGame;
    public GameObject SidescrollerGame;
    public GameObject DonkeyKongGame;
    public GameObject HunterGame;
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
        // GameSelectionMenu.StartGame += StartGame;
        InitializeGame();
    }

    void DeactivateAllGames()
    {
        // PongGame.SetActive(false);
        MeteoriteGame.SetActive(false);
        JumpGame.SetActive(false);
        ShooterGame.SetActive(false);
        BounceGame.SetActive(false);
        SidescrollerGame.SetActive(false);
        DonkeyKongGame.SetActive(false);
        HunterGame.SetActive(false);
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
        DeactivateAllGames();
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
        DeactivateAllGames();
        DeactivateGameMenu();
        DeactivateGameSelectionMenu();
        CurrentState = States.Playing;

        switch (ActiveGame)
        {
            case Games.RocketGame:
                MeteoriteGame.SetActive(true);
                break;
        
            case Games.ShooterGame:
                ShooterGame.SetActive(true);
                break;
            case Games.JumpGame:
                JumpGame.SetActive(true);
                break;
            case Games.BounceGame:
                BounceGame.SetActive(true);
                break;
            case Games.SidescrollerGame:
                SidescrollerGame.SetActive(true);
                break;
            case Games.DonkeyKongGame:
                DonkeyKongGame.SetActive(true);
                break;
            case Games.HunterGame:
                HunterGame.SetActive(true);
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