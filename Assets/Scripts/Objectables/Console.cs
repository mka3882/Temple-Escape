using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Console : Objectable
{
    public Camera fps;
    public Camera topdown;
    public GameObject textboxes;
    bool playedText = false;
    public bool backwards = false;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        if (fps == null || topdown == null)
        {
            fps = Environment.FindFPSCamera();
            topdown = Environment.FindTopDownCamera();
        }
        topdown.enabled = false;
        fps.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        if (fps == null || topdown == null)
        {
            fps = Environment.FindFPSCamera();
            topdown = Environment.FindTopDownCamera();
        }

        if (topdown.enabled == false)
        {
            fps.enabled = false;
            topdown.enabled = true;
        }
        else
        {
            bool puzzleComplete = GameObject.FindGameObjectWithTag("app").GetComponent<PuzzleController>().completed;
            if (textboxes != null && !playedText && (puzzleComplete || !puzzleComplete && backwards))
            {
                textboxes.GetComponent<StoryController>().activated = true;
                textboxes.GetComponent<StoryController>().turnOn = true;
            }
            topdown.enabled = false;
            fps.enabled = true;
        }
    }
}
