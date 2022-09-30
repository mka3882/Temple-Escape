using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Door : Objectable
{
    public GameObject innerDoor;
    public GameObject fpsController;
    public bool open = false;
    public bool startOpen = false;
    public string sceneToOpen;
    public bool noLoad = false;
    public bool lastDoor = false;
    public GameObject textboxes;

    void Start()
    {
        if (startOpen)
        {
            PuzzleSolved();
        }
        if (noLoad)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
    }

    void Update()
    {
        var dist = Mathf.Sqrt(Mathf.Pow(gameObject.transform.position.x - fpsController.transform.position.x, 2) + Mathf.Pow(gameObject.transform.position.z - fpsController.transform.position.z, 2));
        var xDist = Mathf.Abs(gameObject.transform.position.x - fpsController.transform.position.x);
        //Debug.Log("Distance is " + dist);
        if (lastDoor && app.GetComponent<PuzzleController>().completed && textboxes != null && dist <= 4.25)
        {
            textboxes.GetComponent<StoryController>().finished = false;
            textboxes.GetComponent<StoryController>().activated = true;
            textboxes.GetComponent<StoryController>().turnOn = true;
        }
        else if (lastDoor && !app.GetComponent<PuzzleController>().completed && dist <= 4.25)
        {
            SceneManager.LoadScene("End");
        }
        else {
            if (!noLoad && open && dist <= 4.25 /*gameObject.GetComponent<BoxCollider>().bounds.Intersects(fpsController.GetComponent<CharacterController>().bounds)*/)
            {
                //Debug.Log("Entered scene transition");
                /*   if (sceneToOpen > 0)
                   {
                       SceneManager.LoadScene("puzzle" + sceneToOpen);
                   }
                   else
                   {
                       SceneManager.LoadScene("TitleScreen");
                   }*/
                SceneManager.LoadScene(sceneToOpen);

            }
            if (noLoad && open && dist <= 4.25 && textboxes != null)
            {
                textboxes.GetComponent<StoryController>().finished = false;
                textboxes.GetComponent<StoryController>().activated = true;
                textboxes.GetComponent<StoryController>().turnOn = true;
            }
        }
    }

    public void PuzzleSolved()
    {
        if (!open)
        {
            innerDoor.SetActive(false);
            //   Destroy(innerDoor);
            open = true;
            /*  if (textboxes != null)
              {
                  textboxes.GetComponent<StoryController>().activated = true;
                  textboxes.GetComponent<StoryController>().turnOn = true;
              }*/
        }
    }

    public void CloseDoor()
    {
        innerDoor.SetActive(true);
        open = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Entered scene transition");
        //if(gameObject.GetComponent<BoxCollider>().bounds.Intersects())
    }
}

