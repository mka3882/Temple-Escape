using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// End Console/Target/Collider in a level
// Can have multiple in the level
public class TutorialText : Objectable
{
    public GameObject textboxes;
    bool hasBeenTurnedOn = false;

    void Update()
    {
        Camera topdown = Environment.FindTopDownCamera();
        if (topdown.enabled && !hasBeenTurnedOn)
        {
            textboxes.GetComponent<StoryController>().activated = true;
            textboxes.GetComponent<StoryController>().turnOn = true;
            hasBeenTurnedOn = true;
        }
    }
}
