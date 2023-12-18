using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSelectionMenuController : MonoBehaviour
{



    // public GameMenuButtonHandler PlayPongGameButton;
    // public GameMenuButtonHandler PlayRocketGameButton;
    // public GameMenuButtonHandler PlayShooterGameButton;
    // public GameMenuButtonHandler PlayJumpGameButton;

    public RectTransform CalculationCanvas;
    public event System.Action<GameManager.Games> StartGame;
    public float offsetYAxis = 0;
    public List<GameBaseClass> gameBaseClasses;



    private float _canvasHeightNormalized;
    private float _canvasWidthNormalized;
    private float _offsetYAxisPadding = 2.5f;
    private float _offsetYAxisElementHeight = 1;





    void Awake()
    {
        // Initialize pre-render

        _canvasHeightNormalized = CalculationCanvas.rect.height / 2;
        _canvasWidthNormalized = CalculationCanvas.rect.width / 2;

        // Debug.Log("_canvasHeigh: " + _canvasHeightNormalized + "   width: " + _canvasWidthNormalized);

        foreach (GameBaseClass gClass in gameBaseClasses)
        {
            Transform positionOffset = this.transform;
            positionOffset.position = new Vector3(this.transform.position.x, _canvasHeightNormalized - offsetYAxis - _offsetYAxisElementHeight, this.transform.position.z);
            // Debug.Log(positionOffset.position);
            Instantiate(gClass.gameTitleCard, positionOffset.position, positionOffset.rotation);
            offsetYAxis += _offsetYAxisPadding;
        }

    }

}
