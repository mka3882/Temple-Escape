using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : Controller
{
    public override void Init()
    {
        base.Init();
    }

    private void Update()
    {
        #region MOUSE ACTIONS
        if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            // Check KeyCode for Action
            if (Input.GetMouseButtonDown(0)) { }
          //      Debug.Log("Pressed primary button.");

            if (Input.GetMouseButtonDown(1)) { }
                //Debug.Log("Pressed secondary button.");

            if (Input.GetMouseButtonDown(2)) { }
                //Debug.Log("Pressed middle click.");
        }
        
        #endregion

        #region KEYCODE ACTIONS
        if (Input.anyKey)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                // EXIT GAME
                //Debug.Log("Quit");
                UnityEngine.Application.Quit();
            }
            else if (Input.GetKey(KeyCode.Home))
            {
                // GO TO HOME SCREEN
                app.SetGameScene(0);
            }
            else if (Input.GetKey(KeyCode.End))
            {
                // EXIT GAME
                UnityEngine.Application.Quit();
            }
            else if (Input.GetKey(KeyCode.F1))
            {
                // DEBUG SCREEN
            }
        }
        #endregion

    }
}

