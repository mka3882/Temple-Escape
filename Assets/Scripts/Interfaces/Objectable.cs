using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objectable : ApplicationElement
{
    //MOUSE INPUTS ON COLLISIONS
    // ---
    private void OnMouseUp()
    {
        ObjectToConsole("MouseUp");
    }

    private void OnMouseDown()
    {
        ObjectToConsole("MouseDown");
    }

    private void OnMouseOver()
    {
        ObjectToConsole("MouseOver");
    }

    private void OnMouseExit()
    {
        ObjectToConsole("MouseExit");
    }

    private void OnCollisionEnter(Collision collision)
    {
        ObjectToConsole("CollEnter");
    }

    private void OnCollisionExit(Collision collision)
    {
        ObjectToConsole("CollExit");   
    }

    // Print Object To Console
    void ObjectToConsole(string eventAction)
    {
      //  Debug.Log("Player target this obj: " + name + " with : " + eventAction);
    }
}
