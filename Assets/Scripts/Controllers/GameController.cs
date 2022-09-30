using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Controller
{
    CameraManager cameraManager;
    GameState gameState;
    EventController gameEvents;

    public void Start()
    {
        Init();
        app.SetGameScene(0);
    }

    // Init GameController
    // INIT FOR MOST OF GAME
    // CREATES ALL OTHER CONTROLLERS
    public override void Init()
    {
        base.Init();

        // Init CameraManager
        if (cameraManager == null)
        {
            cameraManager = new CameraManager();
        }
    }

    public void InitGameState(int scene, string name)
    {
        // Init GameState
        if (gameState == null)
        {
            gameState = new GameState();
        }

        gameState.CurrentScene = scene;
        gameState.CurrentLevelName = name;

        // Set Gamestate Model
        switch (gameState.CurrentScene)
        {
            default:
                InitWorld();
                InitStart();
                break;
        }
    }

    // Create World
    // Init Base Objects
    // Create All Controllers
    private void InitWorld()
    {
        InitControllers();
    }

    // Intialize Other Controllers
    private void InitControllers()
    {
        app.audioController.Init();
        app.puzzleController.Init();
        app.playerController.Init();

        // Create Event Controller
        gameEvents = gameObject.GetComponent<EventController>();
        gameEvents.Init();
    }

    private void CreateBaseObjects()
    {
        Emitter[] emitterObjs = FindObjectsOfType<Emitter>();
        Reflector[] reflectorObjs = FindObjectsOfType<Reflector>();
        Target[] targetObjs = FindObjectsOfType<Target>();
    }

    // Initlaize the Game
    private void InitStart()
    {
        SpawnCharacter();
    }

    // Set PlayerController Spawn if needed
    // Otherwise Spawn Character
    private void SpawnCharacter()
    {
        app.playerController.SpawnCharacter();
    }
}