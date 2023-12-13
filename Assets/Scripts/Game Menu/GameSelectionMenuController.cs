using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSelectionMenuController : MonoBehaviour
{



    // public GameMenuButtonHandler PlayPongGameButton;
    // public GameMenuButtonHandler PlayRocketGameButton;
    // public GameMenuButtonHandler PlayShooterGameButton;
    // public GameMenuButtonHandler PlayJumpGameButton;

    public event System.Action<GameManager.Games> StartGame;



    public List<GameBaseClass> gameBaseClasses;
    private float offsetXAxis = 4.36f;

    void Awake()
    {

        foreach (GameBaseClass gClass in gameBaseClasses)
        {
            Transform positionOffset = this.transform;
            positionOffset.position = new Vector3(this.transform.position.x + offsetXAxis, this.transform.position.y, this.transform.position.z);
            Debug.Log(positionOffset.position);
            Instantiate(gClass.gameTitleCard, positionOffset.position, positionOffset.rotation);
        }


        // PlayPongGameButton.ButtonActivated += PlayPongGame;
        // PlayRocketGameButton.ButtonActivated += PlayRocketGame;
        // PlayShooterGameButton.ButtonActivated += PlayShooterGame;
        // PlayJumpGameButton.ButtonActivated += PlayJumpGame;
    }

    // void PlayPongGame()
    // {
    //     StartGame?.Invoke(GameManager.Games.PongGame);
    // }
    // 
    // void PlayShooterGame()
    // {
    //     StartGame?.Invoke(GameManager.Games.ShooterGame);
    // }
    // 
    // void PlayRocketGame()
    // {
    //     StartGame?.Invoke(GameManager.Games.RocketGame);
    // }
    // 
    // void PlayJumpGame()
    // {
    //     StartGame?.Invoke(GameManager.Games.JumpGame);
    // }
}
