using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSelectionMenuController : MonoBehaviour
{
    public GameMenuButtonHandler PlayPongGameButton;
    public GameMenuButtonHandler PlayRocketGameButton;
    public GameMenuButtonHandler PlayShooterGameButton;
    public GameMenuButtonHandler PlayJumpGameButton;

    public event System.Action<GameManager.Games> StartGame;

    void Start()
    {
        PlayPongGameButton.ButtonActivated += PlayPongGame;
        PlayRocketGameButton.ButtonActivated += PlayRocketGame;
        PlayShooterGameButton.ButtonActivated += PlayShooterGame;
        PlayJumpGameButton.ButtonActivated += PlayJumpGame;
    }

    void PlayPongGame()
    {
        StartGame?.Invoke(GameManager.Games.PongGame);
    }

    void PlayShooterGame()
    {
        StartGame?.Invoke(GameManager.Games.ShooterGame);
    }

    void PlayRocketGame()
    {
        StartGame?.Invoke(GameManager.Games.RocketGame);
    }

    void PlayJumpGame()
    {
        StartGame?.Invoke(GameManager.Games.JumpGame);
    }
}
