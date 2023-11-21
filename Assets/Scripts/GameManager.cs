using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public enum Orientation
    {
        MeteoriteGame,
        PongGame,
        ShooterGame
    }

    public GameManager.Orientation DefaultActiveGame = Orientation.MeteoriteGame;
    public GameObject PongGame;
    public GameObject MeteoriteGame;
    public GameObject GameMenu;
    public GameObject ShooterGame;
    public GameObject ButtonParent;
    public GameMenuButtonHandler ChangeGameButton;
    public GameMenuButtonHandler RestartButton;
    public GameMenuButtonHandler ContinueButton;
    public FlatscreenPlayerTransformHandler Pointer;
    public float MenuOffsetToPlayer = 1.5f;
    public GameState ThisGameState;

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
        Initialize();
    }

    void ActivateGameMenu(CylinderToFlatscreenPosition _interactingPlayer)
    {
        Debug.Log("activating game menu");
        ThisGameState.CurrentState = (int)GameState.States.Menu;
        GameMenuActivated?.Invoke();
        GameMenu.SetActive(true);
        ButtonParent.transform.position = new Vector3(_interactingPlayer.CorrespondingInGamePlayer.transform.position.x, _interactingPlayer.CorrespondingInGamePlayer.transform.position.y + MenuOffsetToPlayer, transform.position.z);
        Pointer.ThisPlayersController = _interactingPlayer; // tell the gameObject which controller activated this and will therefore control the gameObject
        Pointer.SetPlayerControllersCorrespondingInGamePlayer();
    }

    void DeactivateGameMenu()
    {
        GameMenuDeactivated.Invoke();
        ThisGameState.CurrentState = (int)GameState.States.Playing;
        GameMenu.SetActive(false);
    }

    void Initialize()
    {
        PongGame.SetActive(false);
        MeteoriteGame.SetActive(false);
        DeactivateGameMenu();
        ThisGameState.CurrentState = (int)GameState.States.Playing;

        switch (DefaultActiveGame)
        {
            case Orientation.MeteoriteGame:
                MeteoriteGame.SetActive(true);
                break;
            case Orientation.PongGame:
                PongGame.SetActive(true);
                break;
            default:
                break;
        }
    }

    private void OnDestroy()
    {
        InteractionAreaController.Instance.InteractionCompleted -= ActivateGameMenu;
        ContinueButton.ButtonActivated -= DeactivateGameMenu;
    }
}