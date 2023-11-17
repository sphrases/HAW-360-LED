using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public enum Orientation
    {
        MeteoriteGame,
        PongGame
    }

    public GameManager.Orientation DefaultActiveGame = Orientation.MeteoriteGame;
    public GameObject PongGame;
    public GameObject MeteoriteGame;
    public GameObject GameMenu;
    public FlatscreenPlayerTransformHandler Pointer;

    public event System.Action GameMenuActivated;

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        InteractionAreaController.Instance.Interaction += ActivateGameMenu;
        Initialize();
    }

    void ActivateGameMenu(CylinderToFlatscreenPosition _interactingPlayer)
    {
        GameMenuActivated.Invoke();
        Time.timeScale = 0;
        GameMenu.SetActive(true);
        GameMenu.transform.position = _interactingPlayer.CorrespondingInGamePlayer.transform.position;
        Pointer.ThisPlayersController = _interactingPlayer; // tell the pointer which controller activated the menu and will therefore control the pointer
        Pointer.SetPlayersControllerCorrespondingInGamePlayer();
    }

    void Initialize()
    {
        PongGame.SetActive(false);
        MeteoriteGame.SetActive(false);

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
        InteractionAreaController.Instance.Interaction -= ActivateGameMenu;
    }
}