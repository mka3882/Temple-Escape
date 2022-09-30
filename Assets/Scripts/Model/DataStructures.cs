using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

// Created Data Structrues
// Can be broken up into multiple models
// Created By Caleb Vaccaro

public class GameState
{
    public int CurrentScene = 0;
    public string CurrentLevelName;
    public GameStats gameStats = new GameStats();
}

public class GameStats
{
    int TimeOnGame, CompletedPuzzles, UnCompletedPuzzles;
}

public class CameraManager
{
    Camera MainCamera = Environment.FindMainCamera();
    Camera TopDownCamera = Environment.FindTopDownCamera();
    Camera FPSCamera = Environment.FindFPSCamera();
    Camera SceneCamera = Environment.FindSceneCamera();
}

[SerializeField]
public class EmitterMatricies
{
    public Dictionary<string, int[]> simpleEmittersMatrix = new Dictionary<string, int[]>();
    public Dictionary<string, int[]> complexEmittersMatrix = new Dictionary<string, int[]>();

    public void Init()
    {
        simpleEmittersMatrix.Add("none", new int[] { 0, 0, 0, 0, 0, 0 });
        simpleEmittersMatrix.Add("forward", new int[] { 1, 0, 0, 0, 0, 0 });
        simpleEmittersMatrix.Add("right", new int[] { 0, 1, 0, 0, 0, 0 });
        simpleEmittersMatrix.Add("back", new int[] { 0, 0, 1, 0, 0, 0 });
        simpleEmittersMatrix.Add("left", new int[] { 0, 0, 0, 1, 0, 0 });
        simpleEmittersMatrix.Add("up", new int[] { 0, 0, 0, 0, 1, 0 });
        simpleEmittersMatrix.Add("down", new int[] { 0, 0, 0, 0, 0, 1 });

        complexEmittersMatrix.Add("one", new int[] { 1, 0, 0, 0, 0, 0 });
        complexEmittersMatrix.Add("two", new int[] { 1,
            1,
            0,
            0,
            0,
            0 });
        complexEmittersMatrix.Add("three", new int[] { 1,
            1,
            1,
            0,
            0,
            0});
        complexEmittersMatrix.Add("four", new int[] { 1,
            1,
            1,
            1,
            0,
            0});
        complexEmittersMatrix.Add("five", new int[] { 1,
            1,
            1,
            1,
            1,
            0});
        complexEmittersMatrix.Add("six", new int[] { 1,
            1,
            1,
            1,
            1,
            1});
    }
}