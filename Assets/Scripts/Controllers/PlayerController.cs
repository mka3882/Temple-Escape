using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

//@ Needed To Access FirstPersonController
public class PlayerController : Controller
{
    FirstPersonController firstPersonController;
    GameObject playerGameObject;
    GameObject playerSpawn;

    // Self Reference
    public GameObject CurrentPlayerSpawn
    {
        // Only Set At this Time
        set
        {
            playerSpawn = value;
        }

        get
        {
            return playerSpawn;
        }
    }


    // Self Reference
    public FirstPersonController FPSCharacter
    {
        // Only Set At this Time
        set
        {
            firstPersonController = value;
        }

        get
        {
            if (firstPersonController == null)
            {
                firstPersonController = FindObjectOfType<FirstPersonController>();
            }
            return firstPersonController;
        }
    }

    public void SpawnCharacter()
    {
        firstPersonController.transform.position = playerSpawn.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Init()
    {
        base.Init();

        playerSpawn = GameObject.FindGameObjectWithTag("spawn");
        playerGameObject = GameObject.FindGameObjectWithTag("Player");
        firstPersonController = FindObjectOfType<FirstPersonController>();
    }
}

