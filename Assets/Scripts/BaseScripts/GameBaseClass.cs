using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class GameBaseClass : MonoBehaviour
{
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

}
