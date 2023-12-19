using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongGameManager : MonoBehaviour
{
    public int MaxPlayerAmount;
    public GameObject PlayerPrefab;

    private GameBaseClass gameBaseClass;

    private void Awake()
    {
        gameBaseClass = GetComponent<GameBaseClass>();
    }

    private void Start()
    {
        SpawnPlayers();
    }

    void SpawnPlayers()
    {
        for(int i = 0; (i < gameBaseClass.AvailableControllers.Length) || (i < MaxPlayerAmount); i++)
        {
            GameObject spawnedPlayer = Instantiate(PlayerPrefab, transform);
            spawnedPlayer.GetComponentInChildren<FlatscreenPlayerTransformHandler>().ThisPlayersController = gameBaseClass.AvailableControllers[i].GetComponent<CylinderToFlatscreenPosition>();
        }
    }

    public void StartPongGame() { }

    public void StopPongGame () { }


}
