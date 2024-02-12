using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class GameBaseClass : MonoBehaviour
{
    public int MaxPlayerAmount;
    public GameObject PlayerPrefab;
    public string gameTitle;
    public string gameDescription;
    public GameObject gameTitleCard;
    public Boolean gameCurrentlyRunning;
    public UnityEvent StartGameEvent;
    public UnityEvent StopGameEvent;
    [HideInInspector] public GameObject[] AvailableControllers;

    private void Awake()
    {
        GetAvailableControllers();
        SpawnPlayers();
    }

    private void GetAvailableControllers()
    {
        AvailableControllers = GameObject.FindGameObjectsWithTag("CustomGameController");
    }

    public void HandleStartGame()
    {
        StartGameEvent.Invoke();
    }

    public void HandleStopGame()
    {
        StopGameEvent.Invoke();
    }

    void SpawnPlayers()
    {
        for (int i = 0; (i < AvailableControllers.Length) && (i < MaxPlayerAmount); i++)
        {
            GameObject spawnedPlayer = Instantiate(PlayerPrefab, transform.position, Quaternion.identity, transform);
            spawnedPlayer.GetComponentInChildren<FlatscreenPlayerTransformHandler>().ThisPlayersController = AvailableControllers[i].GetComponent<CylinderToFlatscreenPosition>();
        }
    }

}
