using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryController : Controller
{
    GameObject mainTextbox;
    GameObject[] textboxes;
    public bool finished = false;
    public bool activated = true;
    public bool turnOn = true;
    int currentActive = 0;

    void Awake()
    {
        mainTextbox = gameObject.transform.GetChild(0).gameObject;
        mainTextbox.SetActive(false);
        textboxes = new GameObject[mainTextbox.transform.childCount];
        for(int i = 0; i < mainTextbox.transform.childCount; i++)
        {
            textboxes[i] = mainTextbox.transform.GetChild(i).gameObject;
            textboxes[i].SetActive(false);
        }
    }

    void Update()
    {
        if (!finished && activated)
        {
            if (turnOn)
            {
                mainTextbox.SetActive(true);
                textboxes[0].SetActive(true);
                Debug.Log("Turned on");
                turnOn = false;
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if(currentActive != textboxes.Length - 1)
                {
                    textboxes[currentActive].SetActive(false);
                    currentActive++;
                    textboxes[currentActive].SetActive(true);
                }
                else
                {
                    finished = true;
                    activated = false;
                    mainTextbox.SetActive(false);
                }
            }

        }
    }
}