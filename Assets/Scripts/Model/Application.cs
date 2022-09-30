using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Tangible Application Elemenet and Other Components of the App
/// Caleb V
/// </summary>
public class ApplicationElement : MonoBehaviour
{
    private Application application;

    protected Application app
    {
        get
        {
            if (application == null)
                application = GameObject.FindObjectOfType<Application>();
            return application;
        }
    }
}

public class Application : MonoBehaviour
{
    public GameController gameController;
    public AudioController audioController;
    public PlayerController playerController;
    public PuzzleController puzzleController;

    // Start is called before the first frame update
    void Start()
    {
        // Will Implement when we have a Start and End Menu
        //DontDestroyOnLoad(this.gameObject);
        //Debug.Log(this.name);

        LocalIO.FindDirectory();
    }

    // Load New Scene to ACTIVE
    public void SetGameScene(int scene)
    {
        // Will Implement when we have a Start and End Menu
        // SceneManager.LoadScene(0);
        gameController.InitGameState(scene, SceneManager.GetSceneByBuildIndex(scene).name);
    }
}
