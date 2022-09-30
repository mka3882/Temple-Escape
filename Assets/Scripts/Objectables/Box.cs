using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : Objectable
{
    public GameObject[] reflectors;
    public bool startRaised = false;
    public float lowerHeight = -3.5f;
    Vector3 lowered;
    Vector3 raised;
    bool currentlyRaised = false;

    // Start is called before the first frame update
    void Start()
    {
        // Set Variables
        raised = gameObject.transform.position;
        lowered = gameObject.transform.position;
        lowered.y -= lowerHeight;

        // Set First Position
        if (!startRaised)
        {
            currentlyRaised = false;
            gameObject.transform.position = lowered;
        }
        else
        {
            currentlyRaised = true;
            gameObject.transform.position = raised;
        }
    }

    public void CheckBox(GameObject _reflector)
    {
        foreach (var reflector in reflectors)
        {
            if (_reflector.GetHashCode() == reflector.GetHashCode())
            {
                MoveBox();
            }
        }
    }

    public void MoveBox()
    {
        // check intersection before block is move
        // then wont recheck if its not 
        if (!currentlyRaised)
        {
            currentlyRaised = true;
            gameObject.transform.position = raised;
        }
        else
        {
            currentlyRaised = false;
            gameObject.transform.position = lowered;
        }
    }
}
